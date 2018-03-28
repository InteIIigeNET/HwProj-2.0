using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models.Contexts;

namespace HwProj.Models.Repositories
{
    public class UserManager : BaseManager, IRepository<User>
    {
        public UserManager(ApplicationDbContext context) : base(context)
        {
        }

        public bool Add(User item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User item)
        {
            throw new NotImplementedException();
        }

        public User Get(Func<User, bool> predicate)
        {
            return Context.Users.FirstOrDefault(predicate);
        }

        public IEnumerable<User> GetAll()
        {
	        return Context.Users.Include(c => c.Notifications).ToList();
        }
    }
}