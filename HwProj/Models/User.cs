using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;
using HwProj.Models.Enums;

namespace HwProj.Models
{
	/// <summary>
	/// Модель пользователя сервиса
	/// </summary>
    [Table("Users")]
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
        /// Почта пользователя
        /// </summary>
        public string Email { get; set; }
		/// <summary>
		/// Когда пользователь был создан
		/// </summary>
		public DateTime CreatedAt { get; set; }
		/// <summary>
		/// Когда пользователь в последний раз обновлял о себе информацию
		/// </summary>
		public DateTime UpdatedAt { get; set; }
		/// <summary>
		/// Зашифрованный пароль
		/// </summary>
		public string EncryptedPassword { get; set; }
		/// <summary>
		/// Является ли пользователь преподавателем
		/// </summary>
		public bool IsAdmin { get; set; }
		/// <summary>
		/// Пол пользователя
		/// </summary>
		public Gender   Gender   { get; set; }
        /// <summary>
        /// Коллекция курсов пользователя
        /// </summary>
        public ICollection<Course> Courses { get; set; } = new List<Course>();       
    }
}