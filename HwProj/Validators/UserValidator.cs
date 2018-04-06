using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HwProj.Models;
using Microsoft.AspNet.Identity;

namespace HwProj.Validators
{
	public class UserValidator : UserValidator<User>
	{
		public UserValidator(ApplicationUserManager mgr)
			: base(mgr)
		{
			AllowOnlyAlphanumericUserNames = false;
			RequireUniqueEmail = true;
		}

		public override async Task<IdentityResult> ValidateAsync(User user)
		{
			IdentityResult result = await base.ValidateAsync(user);
			if (user.Name.Any(t => !Char.IsLetter(t)) || user.Surname.Any(t => !Char.IsLetter(t)))
			{
				var errors = result.Errors.Concat(new [] { "Имя и фамилия должны содержать только буквенные значения." });
				result = new IdentityResult(errors);
			}
			return result;
		}
	}
}