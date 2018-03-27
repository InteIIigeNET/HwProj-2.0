﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;

namespace HwProj.Models.Repositories
{
    public class TasksManager : BaseManager, IRepository<Task>
    {
        public TasksManager(ApplicationDbContext context) : base(context)
        {
        }

        public bool Add(Task item)
        {
	        item.Id = Guid.NewGuid();
	        if (Contains(c => c.Id == item.Id)) return false;

	        Context.Tasks.Add(item);
	        Context.SaveChanges();
	        return true;
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
    }
}