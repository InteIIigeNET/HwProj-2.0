using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using HwProj.Models.Enums;
using HwProj.Models.ViewModels;
using HwProj.Models.Roles;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

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
		/// Коллекция курсов пользователя
		/// </summary>
		public ICollection<Course> Courses { get; set; } = new List<Course>();

        /// <summary>
        /// Домашка
        /// </summary>
        public ICollection<Homework> Homeworks { get; set; }
		#endregion

	    public static implicit operator User(RegisterViewModel model)
	    {
			return new User
			{
				UserName = model.Email,
                Name = model.Name,
				Surname = model.Surname,
				Email = model.Email,
				Gender = model.Gender
			};
		}

		public User(): base() { }
	    public User(RegisterViewModel model) : base()
	    {
            UserName = model.Email;
            Name = model.Name;
            Surname = model.Surname;
            Email = model.Email;
            Gender = model.Gender;
        }

	    public static explicit operator EditViewModel(User user)
	    {
		    return new EditViewModel()
		    {
				Id = user.Id,
			    Name = user.Name,
			    Surname = user.Surname,
			    Email = user.Email,
				Role = RolesFactory.GetById(user.Roles.FirstOrDefault().RoleId).Name
		    };
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
            userIdentity.AddClaim(new Claim(ClaimTypes.Email, this.Email));
            userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, this.Name + " " + this.Surname));
            userIdentity.AddClaim(new Claim(ClaimTypes.Role, this.Roles.First().RoleId.GetName()));
			// Здесь добавьте утверждения пользователя
			return userIdentity;
        }
    }
}