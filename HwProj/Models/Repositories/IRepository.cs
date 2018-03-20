using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Repositories
{
	internal interface IRepository<T> : IDisposable
    {
		bool Add(T item);
		bool Delete(T item);
		T Get(Func<T, bool> predicate);
	}
}