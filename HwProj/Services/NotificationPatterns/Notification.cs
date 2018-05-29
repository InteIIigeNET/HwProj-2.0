using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HwProj.Models;

namespace HwProj.Services.NotificationPatterns
{
	public class Notification
	{
        private Func<Task<IEnumerable<long>>> sendNotification;

		protected Notification(IEnumerable<User> users, Func<User, string> buildNotificationFor)
		{
            sendNotification = () => NotificationsService.SendNotifications(users, buildNotificationFor);
		}
		protected Notification(Func<User, bool> usersPredicate, Func<User, string> buildNotificationFor)
		{
            sendNotification = () => NotificationsService.SendNotifications(usersPredicate, buildNotificationFor);
        }
		public Task<IEnumerable<long>> Send()
		{
            return sendNotification();
		}
	}
}