using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Repositories
{
	public interface IRepository<T>
    {
		bool Add(T item);
		bool Delete(T item);
		T Get(Func<T, bool> predicate);
	    IEnumerable<T> GetAll();
        bool Contains(Func<T, bool> predicate);
    }
}