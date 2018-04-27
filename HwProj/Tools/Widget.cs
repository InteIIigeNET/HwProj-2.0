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
			Text = $"<button><a href = \"{UrlGenerator.GetUrl(context, actionName, controllerName, routeValues)}\">{text}</a></button> ";
		}
		public Button(RequestContext context, string text, string actionName, string controllerName)
		{
			Text = $"<button><a href = \"{UrlGenerator.GetUrl(context, actionName, controllerName)}\">{text}</a></button> ";
		}
	}

}