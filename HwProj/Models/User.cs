using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;
using HwProj.Models.Enums;

namespace HwProj.Models
{
	/// <summary>
	/// Модель пользователя сервиса
	/// </summary>
	public class User
	{
		/// <summary>
		/// Уникальный идентификатор
		/// </summary>
		public Guid     Id       { get; set; }
		/// <summary>
		/// Имя пользователя
		/// </summary>
		public string   Name     { get; set; }
		/// <summary>
		/// Фамилия пользователя
		/// </summary>
		public string   Surname  { get; set; }
		/// <summary>
		/// Пол пользователя
		/// </summary>
		public Gender   Gender   { get; set; }
		/// <summary>
		/// Тип пользователя (Student или Teacher)
		/// </summary>
		public UserType UserType { get; set; }

	}
}