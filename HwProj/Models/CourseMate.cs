using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models
{
	/// <summary>
	/// Вспомогательная модель, связывающая курс и его участников
	/// </summary>
	public class CourseMate
	{
		/// <summary>
		/// Идентификатор курса
		/// </summary>
		public int  CourseId { get; set; }
		/// <summary>
		/// Идентификатор одного из участников курса
		/// </summary>
		public Guid UserId   { get; set; }
	}
}