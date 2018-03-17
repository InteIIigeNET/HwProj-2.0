using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Contexts
{
    public class UserDbContext : IdentityDbContext<User>
    {
        public UserDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static UserDbContext Create()
        {
            return new UserDbContext();
        }
    }
}