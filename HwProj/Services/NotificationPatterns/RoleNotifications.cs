using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models;
using HwProj.Tools;

namespace HwProj.Services.NotificationPatterns
{
	public class TeacherAddedNotification : Notification
	{
		public TeacherAddedNotification(User from, User to, HttpRequestBase request)
			: base(new[] { to },
				u => $"Пользователь <b>{from.Name} {from.Surname}</b> ({new MailTo(from.Email)}) указал вас как преподавателя. " +
				     $"Создайте <a href = \"{UrlGenerator.GetRouteUrl(request.RequestContext, "Create", "Courses")}\">свой первый курс</a>")
		{
		}
	}

	public class UserAcceptedInviteNotification : Notification
	{
		public UserAcceptedInviteNotification(string invitedById, User accepted)
			: base(u => u.Id == invitedById,
				u => $"Пользователь <b>{accepted.Name} {accepted.Surname}</b> ({new MailTo(accepted.Email)}) " +
				     $" принял ваше приглашение и успешно зарегистрировался на <b>HwProj</b>")
		{
		}
	}
}