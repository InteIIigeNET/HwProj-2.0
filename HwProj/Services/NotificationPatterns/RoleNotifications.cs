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
				u => $"Пользователь {from.Name} {from.Surname} ({from.Email}) указал вас как преподавателя. " +
				     $"Создайте <a href = \"{UrlGenerator.GetRouteUrl(request.RequestContext, "Create", "Courses")}\">свой первый курс</a>")
		{
		}
	}
}