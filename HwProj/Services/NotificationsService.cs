using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using HwProj.IoC;
using HwProj.Models;
using HwProj.Models.Repositories;
using HwProj.Tools;
using HwProj.Tools.Markdown;
using Ninject;

namespace HwProj.Services
{
    public static class NotificationsService
    {
        private static readonly MainRepository Db = MainRepository.Instance;
        private static readonly IAsyncManager     AsyncManager = Kernel.Instance.Get<IAsyncManager>();

		public static Task<IEnumerable<long>> SendNotifications(Func<User, bool> usersPredicate,
																Func<User, string> buildNotificationFor)
        {
            return SendNotifications(Db.UserManager.GetAll(usersPredicate), buildNotificationFor);
        }

	    public static Task<IEnumerable<long>> SendNotifications(IEnumerable<User> users,
																Func<User, string> buildNotificationFor)
	    {
		    return AsyncManager.Run(() =>
		    {
			    List<long> ids = new List<long>(users.Count());
			    foreach (var user in users)
			    {
				    var notification = new Notification()
				    {
					    IsRead = false,
					    SendingTime = DateTime.Today,
					    Text = buildNotificationFor(user),
					    User = user,
					    UserId = user.Id
				    };
				    Db.NotificationsManager.Add(notification);
					ids.Add(notification.Id);
			    }
			    return ids.AsEnumerable();
		    });
	    }
	}
}