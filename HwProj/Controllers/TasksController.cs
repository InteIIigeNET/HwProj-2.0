using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HwProj.Models.Repositories;
using HwProj.Models.ViewModels;
using HwProj.Services;
using Task = HwProj.Models.Task;

namespace HwProj.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TasksController : Controller
    {
	    readonly MainEduRepository _db = MainEduRepository.Instance;

        [System.Web.Http.HttpPost]
        [System.Web.Mvc.Authorize(Roles = "Преподаватель")]
        public async Task<ActionResult> Create(TaskCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newTask = new Task(model);
            if (_db.TaskManager.Add(newTask))
            {
	            await NotificationsService.SendNotifications(newTask.Course.Users.Select(m => m.User),
		            u => $"В курсе {newTask.Course.Name} добавлено задание {newTask.Title}");

                return RedirectToAction("Index", "Courses", new { courseId = model.CourseId });
            }
            ModelState.AddModelError("", "Не удалось добавить задание");
            return View();
        }

        [System.Web.Mvc.Authorize(Roles = "Преподаватель")]
        public ActionResult Edit(long taskId)
        {
	        if (!ModelState.IsValid)
	        {
		        return PartialView(model);
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