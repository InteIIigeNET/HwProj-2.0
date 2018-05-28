using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models.Enums;
using HwProj.Models.Roles;
using Microsoft.AspNet.Identity;
using System.Reflection.Emit;

namespace HwProj.Models.Contexts
{
	internal class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
		}

		public static AppDbContext Create()
		{
			return new AppDbContext();
		}

		/// <summary>
		/// Все курсы
		/// </summary>
		public DbSet<Course> Courses { get; set; }
		/// <summary>
		/// Все уведомления
		/// </summary>
		public DbSet<Notification> Notifications { get; set; }
		/// <summary>
		/// База всех заданий
		/// </summary>
		public DbSet<Task> Tasks { get; set; }
		/// <summary>
		/// База домашних заданий студентов
		/// </summary>
		public DbSet<Homework> Homeworks { get; set; }
		/// <summary>
		/// Сопоставление курсов и юзеров
		/// </summary>
		public DbSet<CourseMate> CourseMates { get; set; }
        /// <summary>
        /// Данные о пулл реквестах к домашкам
        /// </summary>
        public DbSet<PullRequestData> PullRequestDatas { get; set; }
	}

}