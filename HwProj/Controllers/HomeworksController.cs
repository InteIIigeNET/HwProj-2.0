﻿using HwProj.Models;
using HwProj.Models.Repositories;
using HwProj.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HwProj.Services;
using HwProj.Tools;
using Microsoft.AspNet.Identity;

namespace HwProj.Controllers
{
	[Authorize]
    public class HomeworksController : Controller
    {
        private MainRepository _repository = MainRepository.Instance;

	    [Authorize]
	    public ActionResult Index(long? homeworkId)
	    {
		    if (homeworkId.HasValue)
		    {
			    var homework = _repository.HomeworkManager.Get(h => h.Id == homeworkId.Value);
			    return View(homework);
		    }
			return RedirectToAction("Index", "Home");
		}

		[Authorize]
	    public ActionResult Create(long? taskId, string description)
	    {
		    if (!taskId.HasValue)
		    {
				return RedirectToAction("Index", "Home");
			}
		    return View(new HomeworkCreateViewModel() {TaskId = taskId.Value, Description = description});
	    }


	    [Authorize]
	    [HttpPost]
	    public async Task<ActionResult> Create(HomeworkCreateViewModel model)
	    {
		    if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
                ViewBag.Message = "Ошибка при обновлении базы данных";
                ViewBag.Color = "danger";
            }
            else
		    {
			    var task = _repository.TaskManager.Get(t => t.Id == model.TaskId);
			    var student = _repository.UserManager.Get(u => u.Id == User.Identity.GetUserId());
			    var homework = new Homework(model, task, student);

				if (!_repository.HomeworkManager.Add(homework))
                {
                    ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
                    ViewBag.Message = "Ошибка при обновлении базы данных";
                    ViewBag.Color = "danger";
                }
                else
			    {
				    await NotificationsService.SendNotifications(new [] {task.Course.Mentor},
					    u => $"Пользователь <b>{User.Identity.Name}</b> отправил решение к задаче " +
					         $"<a href = \"{UrlGenerator.GetRouteUrl(Request.RequestContext, "Index", "Homeworks", new { homeworkId = homework.Id})}" +
					         $"\">{task.Title}</>");
                    ViewBag.Message = "Решение было успешно добавлено!";
                    ViewBag.Color = "success";
                }
            }
		    return View();
        }

	    [Authorize(Roles = "Преподаватель")]
		[HttpPost]
		public async Task<ActionResult> AcceptHomework(HomeworkAcceptViewModel model)
	    {
		    if (!ModelState.IsValid)
		    {
			    ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
		    }
		    var homework = _repository.HomeworkManager.Get(h => h.Id == model.HomeworkId);
		    if (!_repository.HomeworkManager.AddReview(User.Identity.GetUserId(), model))
			    ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
		    else
		    {
			    await NotificationsService.SendNotifications(u => u.Id == homework.StudentId,
				    u => $"Задача <b>{homework.Task.Title}</b> проверена <i>(" + (model.IsAccepted
					         ? "зачтена"
					         : $"есть замечания: \"{model.ReviewComment.Substring(0, Math.Min(model.ReviewComment.Length, 15))}...\"") + ")</i>");
		    }
		    return RedirectToAction("Index", "Courses", homework.Task.Course.Id);
		}

	}
}