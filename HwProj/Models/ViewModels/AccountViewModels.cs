using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HwProj.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace HwProj.Models.ViewModels
{
	/// <summary>
	/// Модель регистрации пользователя
	/// </summary>
	public class RegisterViewModel
	{
		/// <summary>
		/// Имя пользователя
		/// </summary>
		[Required]
		[StringLength(20, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
		[Display(Name = "Имя")]
		public string Name { get; set; }

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
		[Display(Name = "Электронная почта")]
		public string Email { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		[Required]
		[StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		/// <summary>
		/// Повтор пароля
		/// </summary>
	    [Required]
		[DataType(DataType.Password)]
		[Display(Name = "Подтверждение пароля")]
		[Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
		public string ConfirmPassword { get; set; }

		/// <summary>
		/// Пол пользователя
		/// </summary>
		[Display(Name = "Пол")]
		[Required]
		public Gender Gender { get; set; }

		/// <summary>
		/// Роль пользователя
		/// </summary>
		[Display(Name = "Роль")]
		public string Role { get; set; } = RoleType.Студент.ToString();

	}

	public class ExternalLoginConfirmationViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Электронная почта")]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Имя")]
		[StringLength(20, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
		public string Name { get; set; }
		[Required]
		[StringLength(20, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 2)]
		[Display(Name = "Фамилия")]
		public string Surname { get; set; }
	}

	public class ExternalLoginListViewModel
	{
		public string ReturnUrl { get; set; }

        public IEnumerable<string> LoginProviders { get; set; }
	}

	public class SendCodeViewModel
	{
		public string SelectedProvider { get; set; }
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
		public string ReturnUrl { get; set; }
		public bool RememberMe { get; set; }
	}

	public class VerifyCodeViewModel
	{
		[Required]
		public string Provider { get; set; }

		[Required]
		[Display(Name = "Код")]
		public string Code { get; set; }

		public string ReturnUrl { get; set; }

		[Display(Name = "Запомнить браузер?")]
		public bool RememberBrowser { get; set; }

		public bool RememberMe { get; set; }
	}

	public class ForgotViewModel
	{
		[Required]
		[Display(Name = "Электронной почты")]
		public string Email { get; set; }
	}

	public class LoginViewModel
	{
		[Required]
		[Display(Name = "Электронная почта")]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Display(Name = "Запомнить меня")]
		public bool RememberMe { get; set; }
	}

	public class ResetPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Электронная почта")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Подтверждение пароля")]
		[Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
		public string ConfirmPassword { get; set; }

		public string Code { get; set; }
	}

	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Электронная почта")]
		public string Email { get; set; }
	}    

}