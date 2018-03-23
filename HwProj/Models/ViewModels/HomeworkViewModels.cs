using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
    public class HomeworkCreateViewModel
    {
        /// <summary>
		/// Название дз
		/// </summary>
		public string Title { get; set; }
        /// <summary>
        /// Описание дз 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
		/// Идентификатор домашнего задания, которому соотвествует 
		/// этот экземпляр выполненного задания 
		/// </summary>        
		public Guid TaskId { get; set; }
    }
}