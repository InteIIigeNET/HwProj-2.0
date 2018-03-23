using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HwProj.Models.ViewModels;

namespace HwProj.Controllers
{
	[Authorize]
    public class TasksController : Controller
    {
	    public ActionResult Create(TaskCreateViewModel model)
	    {
		    if (!ModelState.IsValid)
		    {
			    return View();
		    }
		    return View();
	    }
    }
}