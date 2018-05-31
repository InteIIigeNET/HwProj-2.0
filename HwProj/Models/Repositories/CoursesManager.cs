using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HwProj.Models.Contexts;

namespace HwProj.Models.Repositories
{
    internal class CoursesManager : BaseManager<Course>, IRepository<Course>,
														 IControlWithRights<Course>
	{

        public bool Add(Course item)
        {
	        return Execute
	        (
		        context =>
		        {
			        item.IsComplete = false;
			        if (Contains(c => c.Id == item.Id)) return false;

			        context.Add(item);
			        SaveChanges();
					return true;
		        }
	        );
        }

        public IEnumerable<Course> GetAll()
        {
	        return Execute
	        (
		        context =>
		        {
			        return context.Include(c => c.Mentor)
				        .Include(c => c.Tasks.Select(t => t.Homeworks))
				        .Include(c => c.Users.Select(u => u.User)).ToList();
		        }
			);
        }

        public IEnumerable<Course> GetAll(Func<Course, bool> predicate)
        {
	        return Execute
	        (
		        context =>
		        {
			        return context.Include(c => c.Mentor)
				        .Include(c => c.Tasks.Select(t => t.Homeworks))
				        .Include(c => c.Users.Select(u => u.User)).Where(predicate).ToList();
		        }
	        );
        }

        public bool Contains(Func<Course, bool> predicate)
        {
	        return Execute
			(
		        context =>
		        {
			        return context.Include(c => c.Mentor)
				               .Include(c => c.Tasks)
				               .Include(c => c.Users.Select(u => u.User))
							   .FirstOrDefault(predicate) != null;
		        }
	        );
        }

        public bool Delete(Course item)
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

        public Course Get(Func<Course, bool> predicate)
        {
	        return Execute
	        (
		        context =>
		        {
			        return context.Include(c => c.Mentor)
				        .Include(c => c.Tasks.Select(t => t.Homeworks))
				        .Include(c => c.Users.Select(u => u.User))
				        .FirstOrDefault(predicate);
		        }
	        );
        }

	    public CoursesManager(AppDbContext context) : base(context)
	    {
	    }

		public bool Add(string userRights, Course item)
		{
			return Add(item);
		}

		public bool Delete(string userRights, long objId)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Обнолвляет курс, если пройдена валидация юзера
		/// </summary>
		/// <param name="userRights">Id пользователя</param>
		/// <param name="updateObj">Изменения</param>
		/// <returns>true, если успешно</returns>
		public bool Update(string userRights, Course updateObj)
		{
			return Execute
			(
				context =>
				{
					var course = Get(c => c.Id == updateObj.Id);
					if (course == null) return false;

					if (course.MentorId == userRights)
					{
						course.Name = updateObj.Name;
						course.GroupName = updateObj.GroupName;
						course.IsOpen = updateObj.IsOpen;
						SaveChanges();
						return true;
					}
					return false;
				}
			);
		}

		public bool Update(string userRights, Func<Course, bool> selector, Action<Course> updateAction)
		{
			throw new NotImplementedException();
		}
	}
}