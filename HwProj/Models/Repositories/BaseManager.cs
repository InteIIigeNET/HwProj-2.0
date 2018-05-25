using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models.Interfaces;
using Microsoft.Ajax.Utilities;

namespace HwProj.Models.Repositories
{
    internal abstract class BaseManager<U> where U: class, IModel
    {
	    protected BaseManager(AppDbContext context)
	    {
		    _context = context;
	    }
		private AppDbContext _context;
	    protected T Execute<T>(Func<DbSet<U>, T> action)
	    {
		    var result = action(_context.Set<U>());
		    return result;
	    }

	    protected int SaveChanges()
	    {
		    return _context.SaveChanges();
	    }
    }
}