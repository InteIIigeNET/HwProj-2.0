using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HwProj.Tools
{
	public class UrlGenerator
	{
		public static string GetRouteUrl(RequestContext context, string actionName, string controllerName, object routeValues)
		{
			var urlHelper = new UrlHelper(context);
			return urlHelper.Action(actionName, controllerName, routeValues);
		}

		public static string GetRouteUrl(RequestContext context, string actionName, string controllerName)
		{
			var urlHelper = new UrlHelper(context);
			return urlHelper.Action(actionName, controllerName);
		}
	}
}