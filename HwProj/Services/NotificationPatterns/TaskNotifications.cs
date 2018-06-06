using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using HwProj.Models;
using HwProj.Tools;

namespace HwProj.Services.NotificationPatterns
{
	public class NewTaskNotification : Notification
	{
		public NewTaskNotification(IEnumerable<User> users, Task newTask, RequestContext request)
			: base(users,
				u =>
					$"В курсе <b><a href=\"" +
					$"{UrlGenerator.GetRouteUrl(request, "Index", "Courses", new {courseId = newTask.CourseId})}" +
					$"\">{newTask.Course.Name}</a></b> добавлено задание " +
					$"<a href=\"" +
					$"{UrlGenerator.GetRouteUrl(request, "Create", "Homeworks", new { taskId = newTask.Id, description = newTask.Description })}" +
					$"\">{newTask.Title}</a>")
		{
			
		}
	}
}