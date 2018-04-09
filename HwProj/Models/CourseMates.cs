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
        public long Id { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public long CourseId { get; set; }

        public Course Course { get; set; }
        /// <summary>
        /// Принят ли студент на курс
        /// Для открытых курсов всегда true
        /// </summary>
        public bool IsAccepted { get; set; }
    }
}