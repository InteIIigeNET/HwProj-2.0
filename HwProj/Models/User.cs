﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HwProj.Models.Enums;
using HwProj.Models.Interfaces;
using HwProj.Models.ViewModels;
using HwProj.Models.Roles;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HwProj.Models
{
	/// <summary>
	/// Модель пользователя сервиса
	/// </summary>
    [Table("UsersDbContext")]
    public class User : IdentityUser, IModel
    {
        #region Properties
		public Gender Gender { get; set; }
	    /// <summary>
	    /// Имя пользователя
	    /// </summary>
	    [Required]
	    [Display(Name = "Имя")]
	    public string Name { get; set; }

	    /// <summary>
	    /// Фамилия пользователя
	    /// </summary>
	    [Required]
	    [Display(Name = "Фамилия")]
	    public string Surname { get; set; }
	    /// <summary>
	    /// Коллекция уведомлений пользователя
	    /// </summary>
	    public ICollection<Notification> Notifications { get; set; } 
			= new List<Notification>();
	    public IEnumerable<Notification> NoReadNotifications 
			=> Notifications.Where(n => !n.IsRead).ToList();
	    public IEnumerable<Notification> ReadNotifications
		    => Notifications.Where(n => n.IsRead).ToList();

		/// <summary>
		/// Домашка
		/// </summary>
		public ICollection<Homework> Homeworks { get; set; }
		#endregion

		public User() { }
	    public User(RegisterViewModel model) 
	    {
            UserName = model.Email;
            Name = model.Name;
            Surname = model.Surname;
            Email = model.Email;
            Gender = model.Gender;
        }

		public User(ExternalLoginConfirmationViewModel model)
		{
			UserName = model.Email;
			Email = model.Email;
			Name = model.Name;
			Surname = model.Surname;
		}

		public void EditFrom(EditViewModel model)
		{
			UserName = model.Email;
			Name = model.Name;
			Surname = model.Surname;
			Email = model.Email;
		}


		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

	        userIdentity.AddClaim(new Claim(ClaimTypes.Surname, this.Surname));
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, this.Name));

			var gitToken = this.Claims.FirstOrDefault(t => t.ClaimType.Equals("GitHubAccessToken"));
	        gitToken.IfNotNull(t => userIdentity.AddClaim(new Claim("GitHubAccessToken", t.ClaimValue)));

			userIdentity.AddClaim(new Claim(ClaimTypes.Email, this.Email));
            userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, this.Name + " " + this.Surname));
            userIdentity.AddClaim(new Claim(ClaimTypes.Role, this.Roles.First().RoleId.GetName()));
			// Здесь добавьте утверждения пользователя
			return userIdentity;
        }
    }
}