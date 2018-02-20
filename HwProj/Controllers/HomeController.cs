using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HwProj.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult SignIn()
		{
			ViewBag.Message = "Лучший в СПбГУ - факультет ПМ-ПУ";

			return View();
		}

		public ActionResult SignUp()
		{
			ViewBag.Message = "МатМех - лучше всех!";

			return View();
		}
	}
}