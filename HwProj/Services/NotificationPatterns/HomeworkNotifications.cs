using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models;
using HwProj.Models.ViewModels;
using HwProj.Tools;

namespace HwProj.Services.NotificationPatterns
{
	public class NewHomeworkNotification : Notification
	{
		public NewHomeworkNotification(Task task, User student, Homework homework, HttpRequestBase request)
			: base(new[] { task.Course.Mentor },
				u => $"Пользователь <b>{student.Name} {student.Surname}<b/> ({new MailTo(student.Email)}) отправил решение к задаче " +
				     $"<a href = \"{UrlGenerator.GetRouteUrl(request.RequestContext, "Index", "Homeworks", new { homeworkId = homework.Id })}" +
				     $"\">{task.Title}</a>")
		{
		}
	}

	public class ReviewAddedNotification : Notification
	{
		public ReviewAddedNotification(Homework homework, HomeworkAcceptViewModel model) 
			:base(u => u.Id == homework.StudentId,
				u => $"Задача <b>{homework.Task.Title}</b> проверена <i>(" + (model.IsAccepted
					     ? "зачтена"
					     : $"есть замечания: \"{model.ReviewComment.Substring(0, Math.Min(model.ReviewComment.Length, 15))}...\"") + ")</i>")
		{
		}
	}
}