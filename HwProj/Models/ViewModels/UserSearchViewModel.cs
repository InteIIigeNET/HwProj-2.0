using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HwProj.Models.ViewModels
{
	public class UserSearchViewModel
	{
		public string Email { get; set; }
		public string Description { get; set; }

		public UserSearchViewModel(User model)
		{
			Email = model.Email;
			Description = $"{model.Name} {model.Surname} :: " + model.Email;
		}
	}

	public class UserInviteViewModel
	{
		[Required]
		[DisplayName("Почта")]
		public string Email { get; set; }
		public bool IsTeacher { get; set; }
		[DisplayName("Пригласительное сообщение")]
		public string Message { get; set; }

		public UserInviteViewModel(){}
		public UserInviteViewModel(bool isTeacher)
		{
			IsTeacher = isTeacher;
		}
	}
}