using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HwProj.Models.Interfaces;

namespace HwProj.Models.Repositories
{
	public interface IReadOnlyRepository<out T> where T: class, IModel, new()
	{
		T Get(Func<T, bool> predicate);
		IEnumerable<T> GetAll();
		IEnumerable<T> GetAll(Func<T, bool> predicate);
		bool Contains(Func<T, bool> predicate);
	}

	public interface IRepository<T> : IReadOnlyRepository<T> where T : class, IModel, new()
	{
		bool Add(T item);
		bool Delete(T item);
	}
	public interface IRepositoryWithUpdate<T> : IRepository<T> where T : class, IModel, new()
	{
		bool Update(T updateObj);
		bool Update(Func<T, bool> selector, Action<T> updateAction);
	}

	public interface IBinaryRepository<T, U> : IReadOnlyRepository<U> where U : class, IModel, new()
	{
		bool Add(T item);
		bool Delete(T item);
	}
	public interface IControlWithRights<T> : IReadOnlyRepository<T> where T : class, IModel, new()
	{
		bool Add(string userRights, T item);
		bool Delete(string userRights, long objId);
		bool Update(string userRights, T updateObj);
		bool Update(string userRights, Func<T, bool> selector, Action<T> updateAction);
	}
}