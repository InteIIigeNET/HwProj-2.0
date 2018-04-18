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
        public ActionResult Index(Homework homework)
        {
            return View(homework);
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
					    u => $"Пользователь {User.Identity.Name} отправил решение к задаче {task.Title}");
			    }
		    }
		    return View();
        }
    }
}