using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;

namespace HwProj.Models.Repositories
{
    internal class UserManager : BaseManager<User>, IReadOnlyRepository<User>
    {
	    public IEnumerable<User> GetAll(Func<User, bool> predicate)
	    {
		    return Execute
		    (
			    context => context.Include(u => u.Notifications).Where(predicate).ToList()
			);
	    }

	    public bool Contains(Func<User, bool> predicate)
	    {
		    return Execute
		    (
			    context => Get(predicate) != null
			);
	    }

        public User Get(Func<User, bool> predicate)
        {
	        return Execute
	        (
		        context => context.Include(u => u.Notifications).FirstOrDefault(predicate)
			);
        }

        public IEnumerable<User> GetAll()
        {
	        return Execute
	        (
		        context => context.Include(u => u.Notifications).ToList()
			);
        }

	    public UserManager(AppDbContext context) : base(context)
	    {
	    }
    }
}