using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;

namespace HwProj.Models.Repositories
{
    public class UserManager : BaseManager, IReadOnlyRepository<User>
    {
        public UserManager(ApplicationDbContext context) : base(context)
        {
        }
	    public IEnumerable<User> GetAll(Func<User, bool> predicate)
	    {
			return Context.Users.Include(u => u.Notifications).Where(predicate).AsEnumerable();
		}

	    public bool Contains(Func<User, bool> predicate)
	    {
		    return Get(predicate) != null;
	    }

        public User Get(Func<User, bool> predicate)
        {
            return Context.Users.Include(u => u.Notifications).FirstOrDefault(predicate);
        }

        public IEnumerable<User> GetAll()
        {
	        return Context.Users.Include(u => u.Notifications).AsEnumerable();
        }
    }
}