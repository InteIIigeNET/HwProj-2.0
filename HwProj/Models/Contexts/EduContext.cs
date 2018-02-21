using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HwProj.Models.Contexts
{
    public class EduContext : DbContext
    {
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

        public EduContext() : base() { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasMany(c => c.Users)
                .WithMany(u => u.Courses)
                .Map(t => t.MapLeftKey("CourseId")
                .MapRightKey("UserId")
                .ToTable("CourseMates"));
        }

        /// <summary>
        /// Все зарегистрированные студенты
        /// </summary>
        public IEnumerable<User> Students
        {
            get
            {
                return from u in Users
                       where u.UserType == Enums.UserType.Student
                       select u;                
            }
        }
        /// <summary>
        /// Все зарегистрированные преподаватели
        /// </summary>
        public IEnumerable<User> Teachers
        {
            get
            {
                return from u in Users
                       where u.UserType == Enums.UserType.Teacher
                       select u;                
            }
        }
    }
}