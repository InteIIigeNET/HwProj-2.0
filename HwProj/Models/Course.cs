using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HwProj.Controllers;
using HwProj.Models.ViewModels;

namespace HwProj.Models
{
	//TODO: добавить, закончен ли курс

	/// <summary>
	/// Модель курса занятий
	/// </summary>
    [Table("Courses")]
    public class Course
	{
		/// <summary>
		/// Уникальный идентификатор курса
		/// </summary>
		public Guid Id  { get; set; }
		/// <summary>
		/// Название курса
		/// </summary>
		public string Name     { get; set; }
		/// <summary>
		/// Идентификатор группы, для которой предназначен курс
		/// </summary>
		public string GroupName  { get; set; }
		/// <summary>
		/// Преподаватель
		/// </summary>
		public string MentorName { get; set; }
		/// <summary>
		/// Завершен ли курс?
		/// </summary>
		public bool IsComplete { get; set; }
        /// <summary>
        /// Коллекция пользователей этого курса
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();
        /// <summary>
        /// Таски этого курса
        /// </summary>
        public ICollection<Task> Tasks { get; set; }

		public static implicit operator Course(CreateCourseViewModel model)
		{
			return new Course()
			{
				GroupName = model.GroupName,
				Name = model.Name
			};
		}
    }
}