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
    internal abstract class BaseManager<T> where T: class, IModel
    {
	    private AppDbContext _context;
		protected BaseManager(AppDbContext context)
	    {
		    _context = context;
	    }
	    protected U Execute<U>(Func<DbSet<T>, U> action)
	    {
		    var result = action(_context.Set<T>());
		    return result;
	    }
	    protected int SaveChanges()
	    {
		    return _context.SaveChanges();
	    }
    }
}