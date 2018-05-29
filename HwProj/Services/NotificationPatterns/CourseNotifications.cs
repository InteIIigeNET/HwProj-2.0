using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models;
using HwProj.Tools;

namespace HwProj.Services.NotificationPatterns
{
	public class UserJoinedNotification : Notification
	{
		public UserJoinedNotification(Course course, User joinedUser, HttpRequestBase request)
			: base(new[] {course.Mentor},
				u => $"Пользователь <b>{joinedUser.Name} {joinedUser.Surname}</b> ({new MailTo(joinedUser.Email)}) вступил в курс <b>{course.Name}</b>\n" +
			     (course.IsOpen
				     ? ""
				     : new Button(request.RequestContext, "Принять", "AcceptUser", "Courses",
					       new {courseId = course.Id, userId = joinedUser.Id, notifyId = Models.Notification.ContextId}) +
				       new Button(request.RequestContext, "Отклонить", "RejectUser", "Courses",
					       new {courseId = course.Id, userId = joinedUser.Id, notifyId = Models.Notification.ContextId})))
		{
		}
	}

	public class IsUserAcceptedNotification : Notification
	{
		public IsUserAcceptedNotification(Course course, User acceptedUser, bool isAccepted)
			: base(new[] { acceptedUser },
					u => isAccepted? $"Ваша заявка на курс <b>{course.Name}</b> была принята преподавателем" :
									 $"Ваша заявка на курс <b>{course.Name}</b> была отклонена преподавателем")
		{
		}
	}
}