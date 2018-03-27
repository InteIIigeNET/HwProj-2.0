using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HwProj.Models;
using HwProj.Models.Repositories;
using HwProj.Models.ViewModels;

namespace HwProj.Controllers
{
	[Authorize]
    public class TasksController : Controller
    {
		MainEduRepository Db = MainEduRepository.Instance;

		[HttpPost]
		[Authorize(Roles = "Преподаватель")]
		public ActionResult Create(TaskCreateViewModel model)
	    {
		    if (!ModelState.IsValid)
		    {
			    return View();
		    }
			var newTask = new Task(model);

		    if (Db.TaskManager.Add(newTask))
		    {
			    return RedirectToAction("Index", "Courses", new { courseId = model.CourseId});
		    }
			ModelState.AddModelError("", "Не удалось добавить задание");
		    return View();
	    }
    }
}