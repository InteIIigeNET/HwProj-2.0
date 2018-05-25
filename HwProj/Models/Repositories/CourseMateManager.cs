using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;

namespace HwProj.Models.Repositories
{
	internal class CourseMateManager : BaseManager<CourseMate>,
								     IBinaryRepository<(Course course, User user), CourseMate>

	{
		public CourseMate Get(Func<CourseMate, bool> predicate)
		{
			return Execute
			(
				context => context.Include(m => m.User).FirstOrDefault(predicate)
			);
		}

		public IEnumerable<CourseMate> GetAll()
		{
			return Execute
			(
				context => context.Include(c => c.User)
											  .Include(c => c.Course).ToList()
			);
		}

		public IEnumerable<CourseMate> GetAll(Func<CourseMate, bool> predicate)
		{
			return Execute
			(
				context => context.Include(c => c.User)
											  .Include(c => c.Course)
											  .Where(predicate).ToList()
			);
		}

		public bool Contains(Func<CourseMate, bool> predicate)
		{
			return Execute
			(
				context => Get(predicate) != null
			);
		}

		public bool Add((Course course, User user) item)
		{
			return Execute
			(
				context =>
				{
					var courseMate = new CourseMate(item.course, item.user)
					{
						IsAccepted = item.course.IsOpen
					};

					if (Contains(cm => cm.UserId == item.user.Id &&
					                   cm.CourseId == item.course.Id))
					return false;
					context.Add(courseMate);
					return true;
				}
			);
		}
		public bool Accept((Course course, User user) item)
		{
			return Execute
			(
				context =>
				{
					var courseMate = Get(cm => cm.UserId == item.user.Id &&
					                           cm.CourseId == item.course.Id);
					if (courseMate == null)
						return false;
					courseMate.IsAccepted = true;
					return true;
				}
			);
		}

		public bool Delete((Course course, User user) item)
		{
			return Execute
			(
				context =>
				{
					var courseMate = Get(cm => cm.UserId == item.user.Id &&
					                           cm.CourseId == item.course.Id);
					if (courseMate == null)
						return false;
					context.Remove(courseMate);
					return true;
				}
			);
		}

		public CourseMateManager(AppDbContext context) : base(context)
		{
		}
	}
}