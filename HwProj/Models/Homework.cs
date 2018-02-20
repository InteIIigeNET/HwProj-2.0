using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models
{
	//TODO: добавить номер попытки

	/// <summary>
	/// Модель задание, выполненное студентом
	/// </summary>
	public class Homework
	{
		/// <summary>
		/// Идентификатор домашнего задания, которому соотвествует 
		/// этот экземпляр выполненного задания 
		/// </summary>
		public int  TaskId      { get; set; }
		/// <summary>
		/// Идентификатор студентаЮ выполнившего задание
		/// </summary>
		public Guid StudentId   { get; set; }
		/// <summary>
		/// Зачтена ли данная попытка сдачи выполненного задания
		/// </summary>
		public bool IsCompleted { get; set;  }
	}
}