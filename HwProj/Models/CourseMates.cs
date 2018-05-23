using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HwProj.Models
{
    [Table("CourseMates")]
    public class CourseMate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
		public string UserId { get; set; }
        public virtual User User { get; set; }

        public long CourseId { get; set; }

        public virtual Course Course { get; set; }
        /// <summary>
        /// Принят ли студент на курс
        /// Для открытых курсов всегда true
        /// </summary>
        public bool IsAccepted { get; set; }

		public CourseMate() { }
	    public CourseMate(Course course, User user)
	    {
		    Course = course;
		    User = user;
		    CourseId = Course.Id;
		    UserId = User.Id;
	    }
    }
}