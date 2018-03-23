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
		public string CourseId { get; set; }


		/// <summary>
		/// Описание задания 
		/// </summary>
		public string Description { get; set; }
	}
}