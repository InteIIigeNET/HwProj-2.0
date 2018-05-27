using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;
using HwProj.Models.ViewModels;

namespace HwProj.Models.Repositories
{
    internal class TasksManager : BaseManager<Task>, IControlWithRights<Task>
    {

	    public IEnumerable<Task> GetAll(Func<Task, bool> predicate)
	    {
		    return Execute
		    (
			    context => context.Include(t => t.Course).Include(t => t.Homeworks).ToList()
			);
	    }

	    public bool Contains(Func<Task, bool> predicate)
	    {
		    return Execute
		    (
			    context => Get(predicate) != null
		    );
	    }

	    public bool Delete(Task item)
	    {
		    return Execute
		    (
			    context =>
			    {
				    if (Contains(c => c.Id == item.Id)) return false;
				    context.Remove(item);
					SaveChanges();
					return true;
			    }
		    );
	    }
		public Task Get(Func<Task, bool> predicate)
		{
			return Execute
			(
				context => context.Include(t => t.Course).Include(t => t.Homeworks).FirstOrDefault(predicate)
			);
		}

        public IEnumerable<Task> GetAll()
        {
	        return Execute
	        (
		        context => context.Include(t => t.Course).Include(t => t.Homeworks).ToList()
			);
        }

	    public bool Add(string userRights, Task item)
	    {
		    return Execute
		    (
			    context =>
			    {
				    if (Contains(c => c.Id == item.Id)) return false;
				    if (item.Course.MentorId != userRights) return false;

				    context.Add(item);
					SaveChanges();
					return true;
			    }
		    );
	    }

	    /// <summary>
		/// Удаляет таск, если пройдена валидация юзера
		/// </summary>
		/// <param name="userRights">Id пользователя</param>
		/// <param name="objId">Id удаляемого обьекта</param>
		/// <returns>true, если успешно</returns>
	    public bool Delete(string userRights, long objId)
	    {
		    return Execute
		    (
			    context =>
			    {
				    var task = Get(t => t.Id == objId);
				    if (task == null) return false;

				    if (task.Course.MentorId == userRights)
				    {
					    context.Remove(task);
						SaveChanges();
						return true;
				    }
				    return false;
			    }
			);
	    }

	    /// <summary>
	    /// Обнолвляет таск, если пройдена валидация юзера
	    /// </summary>
	    /// <param name="userRights">Id пользователя</param>
	    /// <param name="updateObj">Изменения</param>
	    /// <returns>true, если успешно</returns>
		public bool Update(string userRights, Task updateObj)
	    {
		    return Execute
		    (
			    context =>
			    {
				    var task = Get(t => t.Id == updateObj.Id);
				    if (task == null) return false;

				    if (task.Course.MentorId == userRights)
				    {
					    task.Description = updateObj.Description;
					    task.Title = updateObj.Title;
						SaveChanges();
						return true;
				    }
				    return false;
			    }
		    );
	    }

	    public TasksManager(AppDbContext context) : base(context)
	    {
	    }
    }
}