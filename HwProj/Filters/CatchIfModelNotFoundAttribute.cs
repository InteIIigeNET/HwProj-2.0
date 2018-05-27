using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HwProj.Filters
{
	public class CatchIfModelNotFoundAttribute : FilterAttribute, IExceptionFilter
	{

		public void OnException(ExceptionContext exceptionContext)
		{
			if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is NullReferenceException)
			{
				exceptionContext.Result = new RedirectResult("/Home/Index");
				exceptionContext.ExceptionHandled = true;
			}
		}
	}
}