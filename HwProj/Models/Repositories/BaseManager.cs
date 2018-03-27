using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Repositories
{
    public class BaseManager
    {
        public BaseManager(ApplicationDbContext context)
        {
            Context = context;
        }

        protected ApplicationDbContext Context { get; }
    }
}