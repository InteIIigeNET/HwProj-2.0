using System;
using System.Collections.Generic;
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
}