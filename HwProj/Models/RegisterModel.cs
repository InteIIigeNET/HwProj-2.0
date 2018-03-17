using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using HwProj.Models.Enums;

namespace HwProj.Models
{
    /// <summary>
    /// Модель регистрации пользователя
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Required]
        public string Surname { get; set; }

		/// <summary>
		/// Почта пользователя
		/// </summary>
		[Required]
		[EmailAddress]
		[Display(Name = "Адрес электронной почты")]
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
		[DataType(DataType.Password)]
		[Display(Name = "Подтверждение пароля")]
		[Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
		public string ConfirmPassword { get; set; }

		/// <summary>
		/// Пол пользователя
		/// </summary>
		[Required]
        public Gender Gender { get; set; }
	}
}