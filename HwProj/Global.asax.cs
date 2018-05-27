using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HwProj.Log;
using NLog;

namespace HwProj
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
		protected void Application_Error()
		{
			var lastException = Server.GetLastError();
			Log.Log.Instance.Error(lastException);
		}

		protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
		{
			if (Request.RequestContext.RouteData.Values["controller"] == null) return;

			var controller = Request.RequestContext.RouteData.Values["controller"]?.ToString();
			var action     = Request.RequestContext.RouteData.Values["action"]?.ToString() ?? "Index";
			var ip         = Request.UserHostAddress;
			var userName   = User.Identity.Name;

			var logInfo = new LogInfo()
			{
				Controller = controller,
				Action     = action,
				UserName   = userName,
				Ip         = ip
			};

			// full route data
			logInfo.RouteData.AddRange(Request.RequestContext.RouteData.Values
									   .Select(item => new KeyValuePair<string, string>
											           (item.Key ?? "", Convert.ToString(item.Value))));
			// Request Query String
			foreach (string key in Request.QueryString.Keys)
			{
				string value = Convert.ToString(Request.QueryString[key]);
				logInfo.RouteData.Add(new KeyValuePair<string, string>(key ?? "", value ?? ""));
			}

			// Request Form Values
			foreach (string key in Request.Form.Keys)
			{
				string value = Convert.ToString(Request.Form[key]);
				logInfo.RouteData.Add(new KeyValuePair<string, string>(key ?? "", value ?? ""));
			}
			Log.Log.Instance.Info(logInfo.ToString());
		}
	}
}
