﻿using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using HwProj.Models.ViewModels;

namespace HwProj.Models.Repositories
{
    internal class HomeworksManager : BaseManager<Homework>, IRepository<Homework>
    {
        public bool Add(Homework item)
        {
	        return Execute
	        (
		        context =>
		        {
			        if (Contains(h => h.Id == item.Id)) return false;
			        var oldAttempts = GetAll(h => h.TaskId    == item.TaskId && 
												  h.StudentId == item.StudentId);

			        if (oldAttempts == null || !oldAttempts.Any()) item.Attempt = 1;
			        else
			        {				        
				        item.Attempt = oldAttempts.Max(t => t.Attempt) + 1;
			        }
			        item.Date = DateTime.Now;
			        context.Add(item);
					SaveChanges();
					return true;
		        }
	        );
        }

	    public IEnumerable<Homework> GetAll(Func<Homework, bool> predicate)
	    {
		    return Execute
		    (
			    context => context.Where(predicate).ToList()
			);
	    }

	    public bool Contains(Func<Homework, bool> predicate)
	    {
		    return Execute
		    (
			    context => Get(predicate) != null
			);
	    }
		/// <summary>
		/// Добавляет рецензию от преподавателя, проверяя права
		/// </summary>
		/// <param name="rights">id пользователя</param>
		/// <param name="model">рецензия от преподавателя</param>
		/// <returns>true, если это тот препод</returns>
	    public bool AddReview(string rights, HomeworkAcceptViewModel model)
	    {
		    return Execute
		    (
			    context =>
			    {
				    var homework = Get(h => h.Id == model.HomeworkId);
				    if (homework == null || homework.Task.Course.Mentor.Id != rights) return false;

				    homework.IsCompleted = model.IsAccepted;
				    homework.ReviewComment = String.IsNullOrEmpty(model.ReviewComment)
											 ? model.IsAccepted? "Задача зачтена" : "Задача не зачтена" 
											 : model.ReviewComment;
					SaveChanges();
					return true;
			    }
		    );
	    }

		public bool Delete(Homework item)
		{
			return Execute
			(
				context =>
				{
					if (!Contains(h => h.Id == item.Id)) return false;
					context.Remove(item);
					SaveChanges();
					return true;
				}
			);
		}

        public Homework Get(Func<Homework, bool> predicate)
        {
	        return Execute
	        (
		        context =>
		        {
			        return context.Include(h => h.Student)
				        .Include(h => h.Task)
				        .Include(h => h.Task.Course).FirstOrDefault(predicate);
		        }
			);
        }

        public IEnumerable<Homework> GetAll()
        {
	        return Execute
	        (
		        context =>
		        {
			        return context.Include(h => h.Student)
				        .Include(h => h.Task)
				        .Include(h => h.Task.Course).ToList();
		        }
	        );
        }

	    public HomeworksManager(AppDbContext context) : base(context)
	    {
	    }
    }
}