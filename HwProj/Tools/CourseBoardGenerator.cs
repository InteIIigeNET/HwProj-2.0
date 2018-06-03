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
		/// <summary>
		/// Генерирует результаты студентов по курсу
		/// </summary>
		/// <param name="course"></param>
		/// <returns></returns>
		public static string GenerateCourseboardHtmlString(this Course course)
		{
			if (!course.Users.Any()) return "";
			var achievments = new Dictionary<long, Dictionary<string, string>>();

			StringBuilder text = new StringBuilder();
			text.AppendLine($"<table border=\"1\">{"Студент".AsHead()}{"TODO".AsHead()}");

			foreach (var task in course.Tasks)
			{
				var title = task.Title;
				text.AppendLine(
					$"{(title.Length > TaskTitleMaxLength ? task.Title.Substring(0, TaskTitleMaxLength) + "..." : task.Title).AsHead()}{"🏆".AsHead()}");
				achievments.Add(task.Id, task.GetTaskAchievements());
			}
			text.Append("</tr>");

			foreach (var user in course.Users.Where(u => u.IsAccepted))
			{
				text.AppendLine($"<tr><td title=\"{user.User.Email}\">{user.User.Name + " " + user.User.Surname}</td>");
				text.AppendLine
					($"<td>{course.Tasks.Sum(t => Convert.ToByte(!t.Homeworks.Where(h => h.IsCompleted).GroupBy(h => h.StudentId).Select(h => h.Key).Contains(user.UserId)))}</td>");
				foreach (var task in course.Tasks)
				{
					text.AppendLine(task.Homeworks.Where(h => h.StudentId == user.UserId).OrderByDescending(h => h.Attempt).FirstOrDefault().GetHomeworkStatusHtmlString());
					text.AppendLine(achievments[task.Id].TryGetValue(user.UserId, out var achievement)?
									$"<td>{achievement}</td>": $"<td></td>");
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
			if (homework == null) return "<td></td>";
			return !homework.IsCompleted ? "<td style=\"background-color: #ccffcc\"></td>" :
										   "<td style=\"background-color: #33cc33\"></td>";
		}

		private static string AsHead(this string title)
		{
			return $"<th style=\"background-color: #007bff; color: white\">{title}</th>";
		}
	}
}