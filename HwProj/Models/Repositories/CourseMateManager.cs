using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Repositories
{
	public class CourseMateManager : BaseManager,
									 IShellRepository<(Course course, User user), CourseMate>

	{
		public bool Contains(Func<(Course course, User user), bool> predicate)
		{
			throw new NotImplementedException();
		}

		public CourseMate Get(Func<(Course course, User user), bool> predicate)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CourseMate> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CourseMate> GetAll(Func<(Course course, User user), bool> predicate)
		{
			throw new NotImplementedException();
		}
	}
}