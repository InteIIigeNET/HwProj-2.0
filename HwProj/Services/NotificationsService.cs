using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models;
using HwProj.Models.Repositories;

namespace HwProj.Services
{
	public static class NotificationsService
	{
		private static MainEduRepository Db = MainEduRepository.Instance;

		public static void SendNotifications(Func<User, bool> usersPredicate, 
											 Func<User, string> buildNotificationFor)
		{
			var users = Db.UserManager.GetAll(usersPredicate);
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
			}
		}
	}
}