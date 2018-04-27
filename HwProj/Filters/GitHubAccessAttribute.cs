using HwProj.Tools;
using System;
using System.Web;
using System.Web.Mvc;

namespace HwProj.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class GitHubAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.User.Identity.GetGitHubToken() == null)
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        {"controller", "Home"},
                        {"action", "Index"}
                    });
            base.OnActionExecuting(filterContext);
        }
    }
}