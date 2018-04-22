using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
    /// <summary>
    /// view для страницы создания курса
    /// </summary>
	public class CreateCourseViewModel
	{
        /// <summary>
        /// Название курса
        /// </summary>
        [Required]
        [Display(Name = "Название курса")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор группы, для которой предназначен курс
        /// </summary>
        [Required]
        [Display(Name = "Название группы")]
        public string GroupName { get; set; }
		/// <summary>
		/// Указывает способ вступления в курс
		/// </summary>
		[Required]
		[Display(Name = "Курс открыт для всех?")]
		public bool IsOpen { get; set; }
	}

    /// <summary>
    /// view для главной страницы курса
    /// </summary>
    public class CourseIndexViewModel
    {
        /// <summary>
        /// Название курса
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор группы, для которой предназначен курс
        /// </summary>
        public string GroupName { get; set; }

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
        public ICollection<User> Users { get; set; }

        /// <summary>
        /// Таски этого курса
        /// </summary>
        public ICollection<Task> Tasks { get; set; }
    }

    /// <summary>
    /// view для отображения курса на других страницах, например, в поиске. 
    /// </summary>
    public class CourseViewModel
    {
        /// <summary>
		/// Уникальный идентификатор курса
		/// </summary>
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        /// <summary>
        /// Название курса
        /// </summary>
        [Display(Name = "Название курса")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор группы, для которой предназначен курс
        /// </summary>
        [Display(Name = "Номер группы")]
        public string GroupName { get; set; }
	    /// <summary>
	    /// Указывает способ вступления в курс
	    /// </summary>
	    [Display(Name = "Доступ по заявкам")]
	    public bool IsOpen { get; set; }

		/// <summary>
		/// Преподаватель
		/// </summary>
		[Display(Name = "Преподаватель")]
        public string MentorName { get; set; }
    }
}