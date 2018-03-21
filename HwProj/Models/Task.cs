using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
		public int    Id          { get; set; }
		/// <summary>
		/// Идентификатор курса, для которого предназначено задание
		/// </summary>
		public Guid CourseId    { get; set; }
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
        public ICollection<Homework> Homeworks { get; set; }
    }
}