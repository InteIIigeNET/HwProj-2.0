using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HwProj.Models;

namespace HwProj.Tools
{
	public abstract class Widget
	{
		protected string Text;
		public static implicit operator string(Widget widget)
		{
			return widget.ToString();
		}
		public override string ToString()
		{
			return Text;
		}
	}

	public class Button : Widget
	{
		public Button(RequestContext context, string text, string actionName, string controllerName, object routeValues)
		{
			Text = $"<button class = \"btn btn-outline - primary\"><a href = \"" +
			       $"{UrlGenerator.GetRouteUrl(context, actionName, controllerName, routeValues)}" +
			       $"\">{text}</a></button> ";
		}
		public Button(RequestContext context, string text, string actionName, string controllerName)
		{
			Text = $"<button class = \"btn btn-outline - primary\"><a href = \"" +
			       $"{UrlGenerator.GetRouteUrl(context, actionName, controllerName)}" +
			       $"\">{text}</a></button> ";
		}
	}

	public class MailTo : Widget
	{
		public MailTo(string email, string title = "")
		{
			Text = $"<a href=\"mailto:{email}?subject={title}\">{email}</a>";
		}
	}

}