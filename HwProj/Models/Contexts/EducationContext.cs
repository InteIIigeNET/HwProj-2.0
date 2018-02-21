using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HwProj.Models.Contexts
{
	public class EducationContext : DbContext
	{
		/// <summary>
		/// База всех пользователей сервиса
		/// </summary>
		public DbSet<User>       Users      { get; set; }
		/// <summary>
		/// База всех курсов сервиса
		/// </summary>
		public DbSet<Course>     Courses    { get; set; }
		/// <summary>
		/// База ключ - значений CoureId - UserId
		/// </summary>
		public DbSet<CourseMate> CourseMates { get; set; }
		/// <summary>
		/// База всех заданий
		/// </summary>
		public DbSet<Task>       Tasks      { get; set; }
		/// <summary>
		/// База всех отправленных домашних заданий
		/// </summary>
		public DbSet<Homework>       Homeworks  { get; set; }

	}
}