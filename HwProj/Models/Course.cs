using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HwProj.Controllers;
using HwProj.Models.ViewModels;
using static System.String;

namespace HwProj.Models
{
	/// <summary>
	/// Модель курса занятий
	/// </summary>
    [Table("Courses")]
    public class Course  : IComparable
	{
        /// <summary>
        /// Уникальный идентификатор курса
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id  { get; set; }
		/// <summary>
		/// Название курса
		/// </summary>
		public string Name     { get; set; }
		/// <summary>
		/// Идентификатор группы, для которой предназначен курс
		/// </summary>
		public string GroupName  { get; set; }
		public string MentorId { get; set; }
		public User Mentor { get; set; }
		/// <summary>
		/// Указывает способ вступления в курс
		/// </summary>
		public bool IsOpen { get; set; }
		/// <summary>
		/// Завершен ли курс?
		/// </summary>
		public bool IsComplete { get; set; }
        /// <summary>
        /// Коллекция пользователей этого курса
        /// </summary>
        public ICollection<CourseMate> Users { get; set; } = new List<CourseMate>();
        /// <summary>
        /// Таски этого курса
        /// </summary>
        public ICollection<Task> Tasks { get; set; } = new List<Task>();

		public Course() { }
		public Course(CreateCourseViewModel model)
		{
			GroupName = model.GroupName;
			Name = model.Name;
			IsOpen = model.IsOpen;
		}

        public int CompareTo(object obj)
        {
            var other = obj as Course;

            if (other == null) return 1;
            int value;
            if ((value = Compare(GroupName, other.GroupName, StringComparison.Ordinal)) != 0)
                return value;
            if ((value = Compare(Name, other.Name, StringComparison.Ordinal)) != 0)
                return value;
            if ((value = Compare(MentorId, other.MentorId, StringComparison.Ordinal)) != 0)
                return value;
            return 0;

		}

        public bool UserExist(string email)
        {
            return Users.FirstOrDefault(u => u.User.Email == email) != null;
        }
    }
}