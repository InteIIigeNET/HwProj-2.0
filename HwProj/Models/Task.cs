using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models
{
	//TODO: Task deadline

	/// <summary>
	/// Модель домашнего задания
	/// </summary>
	public class Task
	{
		/// <summary>
		/// Уникальный идентификатор задания 
		/// </summary>
		public int    Id          { get; set; }
		/// <summary>
		/// Идентификатор курса, для которого предназначено задание
		/// </summary>
		public int    CourseId    { get; set; }
		/// <summary>
		/// Название задания
		/// </summary>
		public string Title       { get; set; }
		/// <summary>
		/// Описание задания 
		/// </summary>
		public string Description { get; set; }
	}
}