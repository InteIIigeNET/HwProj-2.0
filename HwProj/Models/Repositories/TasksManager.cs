using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;
using HwProj.Models.ViewModels;

namespace HwProj.Models.Repositories
{
    public class TasksManager : BaseManager, IRepository<Task>, IControlWithRights<TaskEditViewModel>
    {
        public TasksManager(ApplicationDbContext context) : base(context)
        {
        }

        public bool Add(Task item)
        {
	        if (Contains(c => c.Id == item.Id)) return false;

	        Context.Tasks.Add(item);
	        Context.SaveChanges();
	        return true;
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
	    public bool Delete(long id)
	    {
		    var task = Get(t => t.Id == id);
		    if (task == null) return false;
		    Context.Tasks.Remove(task);
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

	    public bool Update(string userRights, TaskEditViewModel updateObj)
	    {
		    throw new NotImplementedException();
	    }
    }
}