﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HwProj.Models.Repositories;
using HwProj.Models.ViewModels;
using HwProj.Services;
using HwProj.Services.NotificationPatterns;
using HwProj.Tools;
using Microsoft.AspNet.Identity;
using Task = HwProj.Models.Task;

namespace HwProj.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
	    readonly MainRepository _db = MainRepository.Instance;

        [HttpPost]
        [Authorize(Roles = "Преподаватель")]
        public async Task<ActionResult> Create(TaskCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
	        var course = _db.CourseManager.Get(c => c.Id == model.CourseId);
	        var newTask = new Task(model)
	        {
		        CourseId = model.CourseId,
		        Course = course
	        };
            if (_db.TaskManager.Add(User.Identity.GetUserId(), newTask))
            {
	            await (new NewTaskNotification(course.Users.Select(u => u.User), newTask, Request.RequestContext)).Send();
                return RedirectToAction("Index", "Courses", new { courseId = model.CourseId });
            }
            ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
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
				ModelState.AddModelError("", @"Ошибка при обновлении базы данных"); 
				return PartialView("_EditPartial", model);
			}
			return PartialView("TaskPartial", new TaskViewModel(model) { MentorId = User.Identity.GetUserId()});
		}

	    [Authorize(Roles = "Преподаватель")]
        public ActionResult Delete(long taskId, long courseId)
        {
	        if (!_db.TaskManager.Delete(User.Identity.GetUserId(), taskId))
	        {
				ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
			}
			return RedirectToAction("Index", "Courses", new { courseId });
        }
    }
}