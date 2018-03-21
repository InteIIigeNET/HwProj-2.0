using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
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
        [Display(Name = "Номер группы")]
        public string GroupName { get; set; }
	}
}