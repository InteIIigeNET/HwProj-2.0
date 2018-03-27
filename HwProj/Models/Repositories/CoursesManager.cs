using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace HwProj.Models.Repositories
{
    public class CoursesManager : BaseManager, IRepository<Course>
    {
        public CoursesManager(ApplicationDbContext context) : base(context) { }

        public bool Add(Course item)
        {
	        item.IsComplete = false;
			if (Contains(c => c.Id == item.Id)) return false;

		    Context.Courses.Add(item);
	        Context.SaveChanges();
	        return true;
        }

	    public IEnumerable<Course> GetAll()
	    {
		    return Context.Courses.ToList();
	    }

		public bool Contains(Func<Course, bool> predicate)
        {
	        return Context.Courses.FirstOrDefault(predicate) != null;
        }

        public bool Delete(Course item)
        {
			if (Contains(c => c.Id == item.Id)) return false;
	        Context.Courses.Remove(item);
	        Context.SaveChanges();
	        return true;
		}
	
        public Course Get(Func<Course, bool> predicate)
        {
	        return Context.Courses.Include(c => c.Tasks)
								  .Include(c => c.Users)
								  .FirstOrDefault(predicate);
        }
    }
}