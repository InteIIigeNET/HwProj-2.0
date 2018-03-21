using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HwProj.Models.Repositories;

namespace HwProj.Controllers
{
	[RequireHttps]
	public class HomeController : Controller
	{
		MainEduRepository Db = MainEduRepository.Instance;
		public ActionResult Index()
		{
			var couses = Db.CourseManager.GetAll();
			return View(couses);
		}
	}
}