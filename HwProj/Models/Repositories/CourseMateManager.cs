using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;

namespace HwProj.Models.Repositories
{
	public class CourseMateManager : BaseManager,
								     IBinaryRepository<(Course course, User user), CourseMate>

	{
		public CourseMateManager(ApplicationDbContext context) : base(context)
		{
		}

		public CourseMate Get(Func<CourseMate, bool> predicate)
		{
			return Context.CourseMates.FirstOrDefault(predicate);
		}

		public IEnumerable<CourseMate> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CourseMate> GetAll(Func<CourseMate, bool> predicate)
		{
			throw new NotImplementedException();
		}

		public bool Contains(Func<CourseMate, bool> predicate)
		{
			return Get(predicate) != null;
		}

		public bool Add((Course course, User user) item)
		{
			var courseMate = new CourseMate(item.course, item.user)
			{
				IsAccepted = item.course.IsOpen
			};

			if (Contains(cm => cm.UserId == item.user.Id &&
							   cm.CourseId == item.course.Id))
				return false;
			Context.CourseMates.Add(courseMate);
			Context.SaveChanges();
			return true;

		}
		public bool Accept((Course course, User user) item)
		{
			var courseMate = Get(cm => cm.UserId == item.user.Id &&
			                           cm.CourseId == item.course.Id);
			if(courseMate == null)
				return false;
			courseMate.IsAccepted = true;
			Context.SaveChanges();
			return true;

		}

		public bool Delete((Course course, User user) item)
		{
			throw new NotImplementedException();
		}
	}
}