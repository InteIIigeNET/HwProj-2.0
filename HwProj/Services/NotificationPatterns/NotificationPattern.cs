using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HwProj.Models;

namespace HwProj.Services.NotificationPatterns
{
	public class NotificationPattern
	{
		private readonly IEnumerable<User> _users;
		private readonly Func<User, bool> _usersPredicate;
		private readonly Func<User, string> _buildNotificationFor;

		protected NotificationPattern(IEnumerable<User> users, Func<User, string> buildNotificationFor)
		{
			_users = users;
			_buildNotificationFor = buildNotificationFor;
		}
		protected NotificationPattern(Func<User, bool> usersPredicate, Func<User, string> buildNotificationFor)
		{
			_usersPredicate = usersPredicate;
			_buildNotificationFor = buildNotificationFor;
		}
		public Task<IEnumerable<long>> Send()
		{
			return _users != null ? NotificationsService.SendNotifications(_users, _buildNotificationFor) : 
									NotificationsService.SendNotifications(_usersPredicate, _buildNotificationFor);
		}
	}
}