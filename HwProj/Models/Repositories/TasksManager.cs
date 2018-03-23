using System;
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
            throw new NotImplementedException();
        }

        public bool Contains(Func<Task, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Task item)
        {
            throw new NotImplementedException();
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