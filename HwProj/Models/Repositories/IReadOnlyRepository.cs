using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Repositories
{
	public interface IReadOnlyRepository<T>
	{
		T Get(Func<T, bool> predicate);
		IEnumerable<T> GetAll();
		IEnumerable<T> GetAll(Func<T, bool> predicate);
		bool Contains(Func<T, bool> predicate);
	}
	public interface IShellRepository<T, U>
	{
		U Get(Func<T, bool> predicate);
		IEnumerable<U> GetAll();
		IEnumerable<U> GetAll(Func<T, bool> predicate);
		bool Contains(Func<T, bool> predicate);
	}
}