using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HwProj.Tools
{
	public class AsyncManager : IAsyncManager
	{
		public Task Run(Action action)
		{
			return Task.Run(action);
		}
		public Task<U> Run<U>(Func<U> func)
		{
			return Task.Run(func);
		}
	}
}