using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using HwProj.Models.ViewModels;

namespace HwProj.Models.Repositories
{
    public class HomeworksManager : BaseManager, IRepository<Homework>
    {
        public HomeworksManager(ApplicationDbContext context) : base(context) { }

        public bool Add(Homework item)
        {
            if (Contains(h => h.Id == item.Id)) return false;
	        var oldAttempts = GetAll(h => h.TaskId == item.TaskId 
								  && h.StudentId == item.StudentId);

	        if (oldAttempts == null || !oldAttempts.Any())
	        {
		        item.Attempt = 1;
	        }
	        else
	        {
		        var lastAttempt = oldAttempts.FirstOrDefault(h => h.Attempt == oldAttempts.Max(t => t.Attempt));
		        item.Attempt = lastAttempt.Attempt + 1;
		        /* Удаляем старую домашку */
		        Delete(lastAttempt);
	        }
	        item.Date = DateTime.Now;
            Context.Homeworks.Add(item);
            Context.SaveChanges();
            return true;
        }

	    public IEnumerable<Homework> GetAll(Func<Homework, bool> predicate)
	    {
			return Context.Homeworks.Where(predicate).AsEnumerable();
		}

	    public bool Contains(Func<Homework, bool> predicate)
        {
            return Get(predicate) != null;
        }
	    public bool AddReview(HomeworkAcceptViewModel model)
	    {
		    var homework = Get(h => h.Id == model.HomeworkId);
		    if (homework == null) return false;

		    homework.IsCompleted = model.IsAccepted;
		    homework.ReviewComment = model.ReviewComment;
		    Context.SaveChanges();
		    return true;
		}

		public bool Delete(Homework item)
        {
            if (!Contains(h => h.Id == item.Id)) return false;
            Context.Homeworks.Remove(item);
            Context.SaveChanges();
            return true;
        }

        public Homework Get(Func<Homework, bool> predicate)
        {
            return Context.Homeworks.Include(h => h.Student)
									.Include(h => h.Task)
									.Include(h => h.Task.Course).FirstOrDefault(predicate);
        }

        public IEnumerable<Homework> GetAll()
        {
            return Context.Homeworks.Include(h => h.Student)
									.Include(h => h.Task)
									.Include(h => h.Task.Course).AsEnumerable();
        }
    }
}