using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using HwProj.Models;

namespace HwProj.Tools
{
	public static class CourseExtensions
	{
		private const int TaskTitleMaxLength = 10;
		public static string GenerateCourseboardHtmlString(this Course course)
		{
			var achievments = new Dictionary<long, Dictionary<string, string>>();

			StringBuilder text = new StringBuilder();
			text.AppendLine("<table border=\"1\"><tr><th>Студент</th>");

			foreach (var task in course.Tasks)
			{
				var title = task.Title;
				text.AppendLine($"<th>{(title.Length > TaskTitleMaxLength ? task.Title.Substring(0, TaskTitleMaxLength) + "..." : task.Title)}</th>")
					.AppendLine("<th>🏆</th>");
				achievments.Add(task.Id, task.GetTaskAchievements());
			}
			text.Append("</tr>");

			foreach (var user in course.Users.Where(u => u.IsAccepted))
			{
				text.AppendLine($"<tr><th>{user.User.Name + " " + user.User.Surname}</th>");
				foreach (var task in course.Tasks)
				{
					text.AppendLine(task.Homeworks.FirstOrDefault(h => h.StudentId == user.UserId).GetHomeworkStatusHtmlString());
					text.AppendLine(achievments[task.Id].TryGetValue(user.UserId, out var achievement)?
									$"<th>{achievement}</th>": $"<th></th>");
				}
				text.AppendLine("</tr>");
			}
			text.AppendLine("</table>");

			/* Сюда вставить принять/отклонить */
			return text.ToString();
		}
		/// <summary>
		/// Получает список трофеев по заданию
		/// </summary>
		/// <param name="task"></param>
		/// <returns>словарь с key = userId, value = трофей</returns>
		public static Dictionary<string, string> GetTaskAchievements(this Task task)
		{
			var achievements = task.Homeworks.Where(h => h.IsCompleted)
											 .OrderBy(h => h.Date)
											 .Take(3)
											 .Select((h, i) => new KeyValuePair<string, string>
											  (h.StudentId, i == 0? "🥇" : (i == 1? "🥈" : "🥉")))
											 .ToDictionary(h => h.Key, h => h.Value);
			return achievements;
		}

		public static string GetHomeworkStatusHtmlString(this Homework homework)
		{
			if (homework == null) return "<th></th>";
			return !homework.IsCompleted ? "<th style=\"background-color: #ccffcc\"></th>" :
										   "<th style=\"background-color: #33cc33\"></th>";
		}
	}
}