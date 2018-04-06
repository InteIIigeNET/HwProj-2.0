using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.Tools
{
	interface IAsyncManager
	{
		Task Run(Action action);
		Task<U> Run<U>(Func<U> func);
	}
}
