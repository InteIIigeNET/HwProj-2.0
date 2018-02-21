using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HwProj.Models
{
    [Table("Students")]
    public class Student : User
    {
        /// <summary>
        /// Дз пользователя
        /// </summary>
        public ICollection<Homework> Homeworks { get; set; }
    }
}