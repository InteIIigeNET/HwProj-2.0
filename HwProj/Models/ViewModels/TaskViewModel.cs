using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
	public class TaskCreateViewModel
	{
		/// <summary>
		/// Название задания
		/// </summary>
		[Required]
        [DisplayName("Заголовок")]
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
		[Required]
		[StringLength(300, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 10)]
        [DisplayName("Описание")]
        public string Description { get; set; }
	}

	public class TaskEditViewModel
	{
		/// <summary>
		/// Название задания
		/// </summary>
		[Required]
		[DisplayName("Название задания")]
		public string Title { get; set; }
		/// <summary>
		/// Номер задания
		/// </summary>
		public long TaskId { get; set; }
		/// <summary>
		/// номер курса
		/// </summary>
		public long CourseId { get; set; }
		/// <summary>
		/// Описание задания 
		/// </summary>
		[Required]
		[DisplayName("Описание задания")]
		public string Description { get; set; }
		public TaskEditViewModel() { }
		public TaskEditViewModel(TaskViewModel model)
		{
			Title = model.Title;
			Description = model.Description;
			TaskId = model.TaskId;
			CourseId = model.CourseId;
		}
	}
	public class TaskViewModel
	{
		/// <summary>
		/// Название задания
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Просматривает ли таск преподаватель (для возможности редактирования, удаления)
		/// </summary>
		public string MentorId { get; set; }
		/// <summary>
		/// Номер задания
		/// </summary>
		public long TaskId { get; set; }
		/// <summary>
		/// номер курса
		/// </summary>
		public long CourseId { get; set; }
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
			CourseId = task.Course.Id;
			MentorId = task.Course.Mentor.Id;
		}
		public TaskViewModel(TaskEditViewModel model)
		{
			Title = model.Title;
			Description = model.Description;
			TaskId = model.TaskId;
			CourseId = model.CourseId;
		}
	}
}