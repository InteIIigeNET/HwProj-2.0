using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HwProj.Models
{
	//TODO: добавить номер попытки

	/// <summary>
	/// Модель задание, выполненное студентом
	/// </summary>
    [Table("StudentsHomework")]
	public class Homework
	{
        public int Id { get; set; }
		/// <summary>
		/// Идентификатор домашнего задания, которому соотвествует 
		/// этот экземпляр выполненного задания 
		/// </summary>
        
		public int  TaskId      { get; set; }
        public Task Task { get; set; }
		/// <summary>
		/// Идентификатор студента выполнившего задание
		/// </summary>
        
		public Guid UserId   { get; set; }
        public User User { get; set; }
		/// <summary>
		/// Зачтена ли данная попытка сдачи выполненного задания
		/// </summary>
		public bool IsCompleted { get; set;  }
	}
}