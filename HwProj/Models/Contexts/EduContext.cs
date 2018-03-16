using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HwProj.Models.Contexts
{
    public class EduContext : BaseContext
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
    }
}