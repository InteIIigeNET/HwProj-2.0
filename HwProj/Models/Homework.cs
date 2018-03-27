using HwProj.Models.ViewModels;
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
        public User Student { get; set; }
		/// <summary>
		/// Зачтена ли данная попытка сдачи выполненного задания
		/// </summary>
		public bool IsCompleted { get; set;  }
        /// <summary>
		/// Название дз
		/// </summary>
		public string Title { get; set; }
        /// <summary>
        /// Описание дз 
        /// </summary>
        public string Description { get; set; }

        public Homework(HomeworkCreateViewModel model)
        {
            IsCompleted = false;
            Title = model.Title;
            Description = model.Description;
            TaskId = model.TaskId;
        }
    }
}