using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HwProj.Models.ViewModels;

namespace HwProj.Models
{
	//TODO: Task deadline

	/// <summary>
	/// Модель домашнего задания
	/// </summary>
    [Table("Tasks")]
    public class Task
	{
        /// <summary>
        /// Уникальный идентификатор задания 
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long    Id          { get; set; }
		/// <summary>
		/// Идентификатор курса, для которого предназначено задание
		/// </summary>
		public long CourseId    { get; set; }
        //Все для foreign key
        /// <summary>
        /// Курс по данному courseId
        /// </summary>
        public Course Course { get; set; }
		/// <summary>
		/// Название задания
		/// </summary>
		public string Title       { get; set; }
		/// <summary>
		/// Описание задания 
		/// </summary>
		public string Description { get; set; }
        /// <summary>
        /// Дз, в которых есть этот таск
        /// </summary>
        public ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
		public Task(TaskCreateViewModel model) : base()
		{
			CourseId = model.CourseId;
			Description = model.Description;
			Title = model.Title;
		}
		public Task() : base() { }

		public Task(TaskEditViewModel model)
		{
			Description = model.Description;
			Title = model.Title;
			Id = model.TaskId;
		}
	}
}