using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HwProj.Filters
{
	public class CatchIfModelNotFoundAttribute : FilterAttribute, IExceptionFilter
	{

		public void OnException(ExceptionContext exceptionContext)
		{
			if (!exceptionContext.ExceptionHandled && exceptionContext.Exception is NullReferenceException)
			{
				exceptionContext.Result = new RedirectToRouteResult( 
					new RouteValueDictionary()
					{
						{"controller", "Home"},
						{"action", "Index"},
						{"errorMessage", "Ошибка при обновлении базы данных"}
					});
				exceptionContext.ExceptionHandled = true;
			}
		}
	}
}