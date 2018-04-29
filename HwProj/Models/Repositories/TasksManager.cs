using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;
using HwProj.Models.ViewModels;

namespace HwProj.Models.Repositories
{
    public class TasksManager : BaseManager, IControlWithRights<Task>
    {
        public TasksManager(ApplicationDbContext context) : base(context)
        {
        }

	    public IEnumerable<Task> GetAll(Func<Task, bool> predicate)
	    {
		    throw new NotImplementedException();
	    }

	    public bool Contains(Func<Task, bool> predicate)
        {
			return Context.Tasks.FirstOrDefault(predicate) != null;
		}

	    public bool Delete(Task item)
	    {
			if (Contains(c => c.Id == item.Id)) return false;
		    Context.Tasks.Remove(item);
		    Context.SaveChanges();
		    return true;
		}
		public Task Get(Func<Task, bool> predicate)
        {
            return Context.Tasks.FirstOrDefault(predicate);
        }

        public IEnumerable<Task> GetAll()
        {
            throw new NotImplementedException();
        }

	    public bool Add(string userRights, Task item)
	    {
			if (Contains(c => c.Id == item.Id)) return false;

		    var course = Context.Courses.FirstOrDefault(c => c.Id == item.CourseId);
			if (course == null || course.MentorId != userRights) return false;

		    Context.Tasks.Add(item);
		    Context.SaveChanges();
		    return true;
	    }

	    /// <summary>
		/// Удаляет таск, если пройдена валидация юзера
		/// </summary>
		/// <param name="userRights">Id пользователя</param>
		/// <param name="objId">Id удаляемого обьекта</param>
		/// <returns>true, если успешно</returns>
	    public bool Delete(string userRights, long objId)
	    {
		    var task = Get(t => t.Id == objId);
		    if (task == null) return false;

		    if (task.Course.MentorId == userRights)
		    {
			    Context.Tasks.Remove(task);
			    Context.SaveChanges();
			    return true;
		    }
		    return false;
	    }

	    /// <summary>
	    /// Обнолвляет таск, если пройдена валидация юзера
	    /// </summary>
	    /// <param name="userRights">Id пользователя</param>
	    /// <param name="updateObj">Изменения</param>
	    /// <returns>true, если успешно</returns>
		public bool Update(string userRights, Task updateObj)
	    {
			var task = Get(t => t.Id == updateObj.Id);
		    if (task == null) return false;

		    if (task.Course.MentorId == userRights)
		    {
			    task.Description = updateObj.Description;
			    task.Title = updateObj.Title;
				Context.SaveChanges();
			    return true;
		    }
		    return false;
		}
    }
}