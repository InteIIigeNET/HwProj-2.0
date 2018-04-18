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

	public interface IRepository<T> : IReadOnlyRepository<T>
	{
		bool Add(T item);
		bool Delete(T item);
	}

	public interface IBinaryRepository<T, U> : IReadOnlyRepository<U>
	{
		bool Add(T item);
		bool Delete(T item);
	}
}