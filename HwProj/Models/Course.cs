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
		public int    CourseId  { get; set; }
		/// <summary>
		/// Название курса
		/// </summary>
		public string Title     { get; set; }
		/// <summary>
		/// Идентификатор группы, для которой предназначен курс
		/// </summary>
		public string GroupId   { get; set; }
	}
}