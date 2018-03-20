using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
	public class CreateCourseViewModel
	{
		/// <summary>
		/// Название курса
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Идентификатор группы, для которой предназначен курс
		/// </summary>
		public string GroupName { get; set; }
	}
}