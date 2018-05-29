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
    internal abstract class BaseManager<T> where T: class, IModel, new()
    {
	    private readonly AppDbContext _context;
		protected BaseManager(AppDbContext context)
	    {
		    _context = context;
	    }
	    protected TU Execute<TU>(Func<DbSet<T>, TU> action)
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