using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HwProj.Tools
{
	public static class ControllerExtensions
	{
		public static void AddViewBagError(this Controller controller, string errorMessage)
		{
			controller.ModelState.AddModelError("", errorMessage);
			controller.ViewBag.Message = errorMessage;
			controller.ViewBag.Color = "danger";
		}
		public static void AddViewBagMessage(this Controller controller, string message)
		{
			controller.ViewBag.Message = message;
			controller.ViewBag.Color = "success";
		}
	}
}