using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Interfaces;
using HwProj.Models.Contexts;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace HwProj.Models.Courses
{
	public class CoursesManager : IRepository<Course>, IDisposable
	{
		private ApplicationDbContext appDbContext = ApplicationDbContext.Create();
		public bool Create()
		{
			throw new NotImplementedException();
		}

		public bool Delete()
		{
			throw new NotImplementedException();
		}

		public bool Get(Func<Course, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
		}
	}
}