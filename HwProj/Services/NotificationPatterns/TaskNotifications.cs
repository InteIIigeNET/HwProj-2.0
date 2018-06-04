using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models;
using HwProj.Tools;

namespace HwProj.Services.NotificationPatterns
{
	public class NewTaskNotification : Notification
	{
		public NewTaskNotification(IEnumerable<User> users, Task newTask, HttpRequestBase request)
			: base(users,
				u =>
					$"В курсе <a href=\"" +
					$"{UrlGenerator.GetRouteUrl(request.RequestContext, "Index", "Courses", new {courseId = newTask.CourseId})}" +
					$"\">{newTask.Course.Name}</a> добавлено задание " +
					$"<a href=\"" +
					$"{UrlGenerator.GetRouteUrl(request.RequestContext, "Create", "Homeworks", new { taskId = newTask.Id, description = newTask.Description })}" +
					$"\">{newTask.Title}</a>")
		{
			
		}
	}
}