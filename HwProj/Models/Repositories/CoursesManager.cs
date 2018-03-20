using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public bool Contains(Func<Course, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Course item)
        {
            throw new NotImplementedException();
        }

        public Course Get(Func<Course, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}