using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HwProj.Models.Contexts
{
    public class EduContext : DbContext
    {
		public EduContext() : base("DefaultConnection") { }
        /// <summary>
        /// Пользователи (студенты и преподаватели)
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Все курсы
        /// </summary>
        public DbSet<Course> Courses { get; set; }
        /// <summary>
        /// База всех заданий
        /// </summary>
        public DbSet<Task> Tasks { get; set; }
        /// <summary>
        /// База домашних заданий студентов
        /// </summary>
        public DbSet<Homework> Homeworks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasMany(c => c.Users)
                .WithMany(u => u.Courses)
                .Map(t => t.MapLeftKey("CourseId")
                .MapRightKey("UserId")
                .ToTable("CourseMates"));
        }
    }
}