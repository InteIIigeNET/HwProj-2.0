using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace HwProj.Filters
{
	public class AnonymousOnlyAttribute : FilterAttribute, IAuthenticationFilter
	{
		public void OnAuthentication(AuthenticationContext filterContext)
		{
			var user = filterContext.HttpContext.User;
			if (user != null && user.Identity.IsAuthenticated)
			{
				filterContext.Result = new RedirectToRouteResult(
					new System.Web.Routing.RouteValueDictionary
					{
						{"controller", "Home"},
						{"action", "Index"}
					});
			}
		}

		public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
		{
			var user = filterContext.HttpContext.User;
			if (user != null && user.Identity.IsAuthenticated)
			{
				filterContext.Result = new RedirectToRouteResult(
					new System.Web.Routing.RouteValueDictionary
					{
						{"controller", "Home"},
						{"action", "Index"},
					});
			}
		}
	}
}