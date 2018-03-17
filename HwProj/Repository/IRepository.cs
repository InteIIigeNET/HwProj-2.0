using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.Repository
{
	interface IRepository<T> : IDisposable where T: class
	{
		bool Add(T item);
		bool Remove(T item);
		bool Update(T item);
		T Get(Func<T, bool> check);
	}
}
