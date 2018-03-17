using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using HwProj.Models.Enums;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HwProj.Models
{
	/// <summary>
	/// Модель пользователя сервиса
	/// </summary>
    [Table("Users")]
    public class User : IdentityUser
    {
        #region Properties
		public Gender Gender { get; set; }
        /// <summary>
        /// Коллекция курсов пользователя
        /// </summary>
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        #endregion

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }
}