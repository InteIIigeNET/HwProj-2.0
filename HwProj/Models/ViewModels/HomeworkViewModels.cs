using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
    public class HomeworkCreateViewModel
    {
	    [Display(Name = "Условие задания:")]
	    public string Description { get; set; }
		[Display(Name = "Комментарий")]
		public string Comment { get; set; }
		[Required]
	    [Display(Name = "Ссылка на код на GitHub")]
		public string GitHub { get; set; }
		public int Attempt { get; set; }
		public long TaskId { get; set; }
    }

	public class HomeworkAcceptViewModel
	{
		[Display(Name = "Комментарий")]
		public string ReviewComment { get; set; }
		[Required]
		[Display(Name = "Задача принята")]
		public bool IsAccepted { get; set; }
		public long HomeworkId { get; set; }
	}

}