using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Interfaces
{
	internal interface IRepository<T>
	{
		bool Create();
		bool Delete();
		bool Get(Func<T, bool> predicate);
	}
}