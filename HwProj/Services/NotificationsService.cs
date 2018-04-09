//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Threading.Tasks;
//using HwProj.Models;
//using HwProj.Models.Repositories;
//using HwProj.Tools;

//namespace HwProj.Services
//{
//	public static class NotificationsService
//	{
//		private static readonly MainEduRepository Db = MainEduRepository.Instance;
//		private static readonly IAsyncManager AsyncManager = new AsyncManager();

//		public static System.Threading.Tasks.Task SendNotifications(Func<User, bool> usersPredicate,
//			Func<User, string> buildNotificationFor)
//		{
//			return AsyncManager.Run(()=>
//			{
//				var users = Db.UserManager.GetAll(usersPredicate);
//				foreach (var user in users)
//				{
//					var notification = new Notification()
//					{
//						IsRead = false,
//						SendingTime = DateTime.Today,
//						Text = buildNotificationFor(user),
//						User = user,
//						UserId = user.Id
//					};
//					Db.NotificationsManager.Add(notification);
//				}
//			});
//		}
//	}
//}