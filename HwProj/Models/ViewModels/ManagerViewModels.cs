using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HwProj.Models.Roles;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace HwProj.Models.ViewModels
{

	public class IndexViewModel
	{
		public bool HasPassword { get; set; }
		public IList<UserLoginInfo> Logins { get; set; }
		public string PhoneNumber { get; set; }
		public bool TwoFactor { get; set; }
		public bool BrowserRemembered { get; set; }
	}

	public class ManageLoginsViewModel
	{
		public IList<UserLoginInfo> CurrentLogins { get; set; }
		public IList<AuthenticationDescription> OtherLogins { get; set; }
	}

	public class FactorViewModel
	{
		public string Purpose { get; set; }
	}

	public class SetPasswordViewModel
	{
		[Required]
		[StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Новый пароль")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Подтверждение нового пароля")]
		[Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
		public string ConfirmPassword { get; set; }
	}

	public class ChangePasswordViewModel
	{
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Текущий пароль")]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Новый пароль")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Подтверждение нового пароля")]
		[Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
		public string ConfirmPassword { get; set; }
	}

	public class AddPhoneNumberViewModel
	{
		[Required]
		[Phone]
		[Display(Name = "Номер телефона")]
		public string Number { get; set; }
	}

	public class VerifyPhoneNumberViewModel
	{
		[Required]
		[Display(Name = "Код")]
		public string Code { get; set; }

		[Required]
		[Phone]
		[Display(Name = "Номер телефона")]
		public string PhoneNumber { get; set; }
	}

	public class ConfigureTwoFactorViewModel
	{
		public string SelectedProvider { get; set; }
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
	}

	public class EditViewModel
	{
		/// <summary>
		/// Имя пользователя
		/// </summary>
		[Required]
		[StringLength(20, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
		[Display(Name = "Имя")]
		public string Name { get; set; }

		[Display(Name = "Статус")]
		public string Role { get; set; }

		/// <summary>
		/// Фамилия пользователя
		/// </summary>
		[Required]
		[StringLength(20, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
		[Display(Name = "Фамилия")]
		public string Surname { get; set; }

		/// <summary>
		/// Почта пользователя
		/// </summary>
		[Required]
		[EmailAddress]
		[Display(Name = "E-Mail")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		[StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
		public string Password { get; set; }
		public string Id { get; set; }

		[StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Новый пароль")]
		public string NewPassword { get; set; }

		/// <summary>
		/// Повтор пароля
		/// </summary>
		[DataType(DataType.Password)]
		[Display(Name = "Подтверждение пароля")]
		[Compare("NewPassword", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
		public string ConfirmPassword { get; set; }
	}
}