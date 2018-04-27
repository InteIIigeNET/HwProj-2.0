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

namespace HwProj.Controllers
{
	[Authorize]
    public class HomeworksController : Controller
    {
        private MainEduRepository EduRepository = MainEduRepository.Instance;

	    [Authorize]
	    public ActionResult Index(long? homeworkId)
	    {
		    if (homeworkId.HasValue)
		    {
			    var homework = EduRepository.HomeworkManager.Get(h => h.Id == homeworkId.Value);
			    return View(homework);
		    }
		    return View();
	    }

		[Authorize]
	    public ActionResult Create(long? taskId, string description)
	    {
		    if (!taskId.HasValue)
		    {
			    throw new Exception();
			    //перенаправить
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
			    var task = EduRepository.TaskManager.Get(t => t.Id == model.TaskId);
			    var student = EduRepository.UserManager.Get(u => u.Email == User.Identity.Name);

			    if (!EduRepository.HomeworkManager.Add(new Homework(model, task, student)))
				    ModelState.AddModelError("", "Ошибка");
			    else
			    {
				    await NotificationsService.SendNotifications(u => u.Email == task.Course.MentorsEmail,
					    u => $"Пользователь {User.Identity.Name} отправил решение к задаче <a>{task.Title}</>");
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
		    var homework = EduRepository.HomeworkManager.Get(h => h.Id == model.HomeworkId);
			if (homework.Task.Course.MentorsEmail != User.Identity.Name)
		    {
				/* Если не тот ментор */
			    return View();
		    }
		    if (!EduRepository.HomeworkManager.AddReview(model))
			    ModelState.AddModelError("", "Ошибка");
		    else
		    {
			    await NotificationsService.SendNotifications(u => u.Id == homework.StudentId,
				    u => $"Задача {homework.Task.Title} проверена (" + (model.IsAccepted
					         ? "зачтена"
					         : $"есть замечания: \"{model.ReviewComment.Substring(0, Math.Min(model.ReviewComment.Length, 15))}...\"") + ")");
		    }
		    return View(); //пофиксить
	    }

	}
}