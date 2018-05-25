using HwProj.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HwProj.Models.Interfaces;

namespace HwProj.Models
{
	//TODO: добавить номер попытки

	/// <summary>
	/// Модель задание, выполненное студентом
	/// </summary>
    [Table("StudentsHomework")]
	public class Homework : IModel
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
		/// <summary>
		/// Идентификатор домашнего задания, которому соотвествует 
		/// этот экземпляр выполненного задания 
		/// </summary>        
		public long  TaskId      { get; set; }
        // Все для foreign key
        /// <summary>
        /// Таск по этому taskId
        /// </summary>
        public Task Task { get; set; }
		/// <summary>
		/// Идентификатор студента выполнившего задание
		/// </summary>
		public string StudentId   { get; set; }
        // Все для foreign key
        /// <summary>
        /// Пользователь по этому userId
        /// </summary>
        [ForeignKey("StudentId")]
        public User Student { get; set; }
		/// <summary>
		/// Зачтена ли данная попытка сдачи выполненного задания
		/// </summary>
		public bool IsCompleted { get; set;  }
		/// <summary>
		/// Комментарий к решению
		/// </summary>
		public string Comment { get; set; }
		/// <summary>
		/// Ссылка на код на GitHub
		/// </summary>
		public string GitHub { get; set; }
		/// <summary>
		/// Дата отправки
		/// </summary>
		public DateTime? Date { get; set; }
		/// <summary>
		/// Номер попытки
		/// </summary>
		public int Attempt { get; set; }
		/// <summary>
		/// Комментарий от преподавателя
		/// </summary>
		public string ReviewComment { get; set; }
		public Homework() { }
		public Homework(HomeworkCreateViewModel model, Task task, User student)
        {
            IsCompleted = false;
	        Comment = model.Comment;
	        GitHub = model.GitHub;
	        Task = task;
	        TaskId = task.Id;
	        Student = student;
	        StudentId = student.Id;
        }
    }
}