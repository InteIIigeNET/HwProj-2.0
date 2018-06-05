using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using HwProj.Models;
using HwProj.Models.ViewModels;
using HwProj.Tools;

namespace HwProj.Services.NotificationPatterns
{
	public class NewHomeworkNotification : Notification
	{
		public NewHomeworkNotification(Task task, User student, Homework homework, RequestContext request)
			: base(new[] { task.Course.Mentor },
				u => $"Пользователь <b>{student.Name} {student.Surname}</b> ({new MailTo(student.Email)}) отправил решение к задаче " +
				     $"<a href = \"{UrlGenerator.GetRouteUrl(request, "Index", "Homeworks", new { homeworkId = homework.Id })}" +
				     $"\">{task.Title}</a>")
		{
		}
	}

	public class ReviewAddedNotification : Notification
	{
		public ReviewAddedNotification(Homework homework, HomeworkAcceptViewModel model, RequestContext request) 
			: base(u => u.Id == homework.StudentId,
				u => $"Задача <a href = \"{UrlGenerator.GetRouteUrl(request, "Index", "Homeworks", new { homeworkId = homework.Id })}" +
				     $"\">{homework.Task.Title}</a> проверена <i>(" + (model.IsAccepted
					     ? "зачтена"
					     : $"есть замечания: \"{model.ReviewComment.Substring(0, Math.Min(model.ReviewComment.Length, 15))}...\"") + ")</i>")
		{
		}
	}

    public class NewPullRequestHomeworkNotification : Notification
    {
        public NewPullRequestHomeworkNotification(Task task, User student, long pullRequestDataId, RequestContext request) 
            : base(new[] { task.Course.Mentor },
                u => $"Пользователь <b>{student.Name} {student.Surname} ({student.Email})</b> отправил решение к задаче " +
                     $"<a href = \"" +
                     $"{UrlGenerator.GetRouteUrl(request, "Index", "PullRequest", new { pullRequestDataId })}" +
                     $"\">{task.Title}</a>")
        {
        }
    }
}