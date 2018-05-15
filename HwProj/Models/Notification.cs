using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HwProj.Models
{
	public class Notification
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
		public string Text { get; set; }
		public bool IsRead { get; set; }
		public DateTime? SendingTime { get; set; }

		public const string ContextId = "contextNotifyId";
	}
}