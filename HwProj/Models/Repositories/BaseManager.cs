using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Repositories
{
    internal abstract class BaseManager
    {
        protected BaseManager()
        {
        }
	    protected T Execute<T>(Func<AppDbContext, T> action)
	    {
		    using (var context = AppDbContext.Create())
		    {
			    //context.Configuration.LazyLoadingEnabled = false;
				return action(context);
		    }
	    }
	}
}