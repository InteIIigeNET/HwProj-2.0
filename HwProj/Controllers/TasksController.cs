using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HwProj.Models.Repositories;
using HwProj.Models.ViewModels;
using HwProj.Services;
using Microsoft.AspNet.Identity;
using Task = HwProj.Models.Task;

namespace HwProj.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
	    readonly MainEduRepository _db = MainEduRepository.Instance;

        [HttpPost]
        [Authorize(Roles = "Преподаватель")]
        public async Task<ActionResult> Create(TaskCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newTask = new Task(model);
            if (_db.TaskManager.Add(User.Identity.GetUserId(), newTask))
            {
	            await NotificationsService.SendNotifications(newTask.Course.Users.Select(m => m.User),
		            u => $"В курсе {newTask.Course.Name} добавлено задание {newTask.Title}");

                return RedirectToAction("Index", "Courses", new { courseId = model.CourseId });
            }
            ModelState.AddModelError("", "Не удалось добавить задание");
            return View();
        }

	    [Authorize(Roles = "Преподаватель")]
	    public ActionResult _EditPartial(TaskEditViewModel model)
	    {
			return PartialView(model);
		}

		[Authorize(Roles = "Преподаватель")]
        public ActionResult Edit(TaskEditViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return PartialView("_EditPartial", model);
			}
			if (!_db.TaskManager.Update(User.Identity.GetUserId(), new Task(model)))
			{
				ModelState.AddModelError("", "Ошибка при редактировании задания"); 
				return PartialView("_EditPartial", model);
			}
			return PartialView("TaskPartial", new TaskViewModel(model));
		}

	    [Authorize(Roles = "Преподаватель")]
        public ActionResult Delete(long taskId, long courseId)
        {
	        if (!_db.TaskManager.Delete(User.Identity.GetUserId(), taskId))
	        {
				ModelState.AddModelError("", "Не удалось удалить задание");
			}
			return RedirectToAction("Index", "Courses", new { courseId });
        }
    }
}