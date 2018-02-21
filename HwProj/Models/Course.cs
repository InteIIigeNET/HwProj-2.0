using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
		public int    Id  { get; set; }
		/// <summary>
		/// Название курса
		/// </summary>
		public string Name     { get; set; }
		/// <summary>
		/// Идентификатор группы, для которой предназначен курс
		/// </summary>
		public string GroupName  { get; set; }
        /// <summary>
        /// Завершен ли курс?
        /// </summary>
        public bool IsComplete { get; set; }
        /// <summary>
        /// Коллекция пользователей этого курса
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}