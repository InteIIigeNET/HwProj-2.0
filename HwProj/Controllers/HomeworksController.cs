using HwProj.Models;
using HwProj.Models.Repositories;
using HwProj.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HwProj.Services;
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
			    ModelState.AddModelError("", "Нужно заполнить все поля");
		    }
		    else
		    {
			    var task = _repository.TaskManager.Get(t => t.Id == model.TaskId);
			    var student = _repository.UserManager.Get(u => u.Email == User.Identity.Name);

			    if (!_repository.HomeworkManager.Add(new Homework(model, task, student)))
				    ModelState.AddModelError("", "Ошибка");
			    else
			    {
				    await NotificationsService.SendNotifications(new [] {task.Course.Mentor},
					    u => $"Пользователь <b>{User.Identity.Name}</b> отправил решение к задаче <a>{task.Title}</>");
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
			    ModelState.AddModelError("", "Нужно заполнить все поля");
		    }
		    var homework = _repository.HomeworkManager.Get(h => h.Id == model.HomeworkId);
			if (homework.Task.Course.MentorId != User.Identity.GetUserId())
		    {
				// Не показываем, что аккаунт не ментора 
			    return RedirectToAction("Index", "Home");
			}
		    if (!_repository.HomeworkManager.AddReview(model))
			    ModelState.AddModelError("", "Ошибка при добавлении комментария");
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