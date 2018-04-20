using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HwProj.Models;
using HwProj.Models.Repositories;
using HwProj.Models.ViewModels;

namespace HwProj.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TasksController : Controller
    {
	    readonly MainEduRepository _db = MainEduRepository.Instance;

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize(Roles = "Преподаватель")]
        public ActionResult Create(TaskCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newTask = new Task(model);
            if (_db.TaskManager.Add(newTask))
            {
                return RedirectToAction("Index", "Courses", new { courseId = model.CourseId });
            }
            ModelState.AddModelError("", "Не удалось добавить задание");
            return View();
        }

        [System.Web.Mvc.Authorize(Roles = "Преподаватель")]
        public void Edit(Guid taskId)
        {
            if (!ModelState.IsValid)
            {
                //return View();
            }

        }

        [System.Web.Http.Authorize(Roles = "Преподаватель")]
        public ActionResult Delete(long? taskId, long? courseId)
        {
            if (taskId == null || !_db.TaskManager.Delete(taskId.Value))
            {
                ModelState.AddModelError("", "Не удалось удалить задание");
            }
            return RedirectToAction("Index", "Courses", new { courseId });
        }
    }
}