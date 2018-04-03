using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HwProj.Tools
{
	public static class AsyncManager
	{
		public static Task Run(Action action)
		{
			return Task.Run(action);
		}
		public static Task<U> Run<U>(Func<U> func)
		{
			return Task.Run(func);
		}
	}
}