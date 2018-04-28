using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
	public class TaskCreateViewModel
	{
		/// <summary>
		/// Название задания
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Номер курса
		/// </summary>
		public long CourseId { get; set; }
		/// <summary>
		/// Курс по данному courseId
		/// </summary>
		public Course Course { get; set; }
		/// <summary>
		/// Описание задания 
		/// </summary>
		public string Description { get; set; }
	}

	public class TaskEditViewModel
	{
		/// <summary>
		/// Название задания
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Номер задания
		/// </summary>
		public long TaskId { get; set; }
		/// <summary>
		/// Описание задания 
		/// </summary>
		public string Description { get; set; }
	}
	public class TaskViewModel
	{
		/// <summary>
		/// Название задания
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Номер задания
		/// </summary>
		public long TaskId { get; set; }
		/// <summary>
		/// Описание задания 
		/// </summary>
		public string Description { get; set; }

		public TaskViewModel() { }

		public TaskViewModel(Task task)
		{
			Title = task.Title;
			Description = task.Description;
			TaskId = task.Id;
		}
		public TaskViewModel(TaskEditViewModel model)
		{
			Title = model.Title;
			Description = model.Description;
			TaskId = model.TaskId;
		}
	}
}