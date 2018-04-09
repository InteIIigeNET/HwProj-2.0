using HwProj.Models;
using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HwProj.Models.Enums;
using HwProj.Models.ViewModels;
using HwProj.Models.Repositories;
using HwProj.Services;
using HwProj.Tools;
using Microsoft.AspNet.Identity;

namespace HwProj.Controllers
{
	[Authorize]
    public class CoursesController : Controller
    {
	    private MainEduRepository EduRepository = MainEduRepository.Instance;

	    [Authorize(Roles = "Преподаватель")]
		public ActionResult Create()
        {            
            return View();
        }

	    [Authorize(Roles = "Преподаватель")]
	    [HttpPost]
	    public ActionResult Create(CreateCourseViewModel courseView)
	    {
		    if (!ModelState.IsValid)
		    {
			    return View();
		    }
		    var course = (Course) courseView;
            course.MentorsName = User.Identity.GetUserFullName();
            course.MentorsEmail = User.Identity.GetUserEmail();

			if (EduRepository.CourseManager.Contains(t => t.CompareTo(course) == 0))
			    ModelState.AddModelError("", "Курс с таким описанием уже существует");
		    else
			{
				if (EduRepository.CourseManager.Add(course))
					return RedirectToAction("Index", new { courseId = course.Id});

				else ModelState.AddModelError("", "Ошибка при созданни курса. Повторите попытку.");
			}
		    return View();
	    }

        [AllowAnonymous]
	    public ActionResult Index(long? courseId)
        {
            if (courseId != null)
                return View(EduRepository.CourseManager.Get(c => c.Id == courseId));
            else
                return View("CoursesList", EduRepository.CourseManager.GetAll());
        }

	    [Authorize(Roles = "Преподаватель")]
	    public ActionResult Edit()
	    {
		    return null;
	    }

	    [Authorize(Roles = "Преподаватель")]
	    public ActionResult AddTask(long? courseId)
	    {
		    if (courseId == null) return View("CoursesList");

		    var course = EduRepository.CourseManager.Get(t => t.MentorsEmail == User.Identity.Name);
			if(course == null) return View("CoursesList");

		    return View("~/Views/Tasks/Create.cshtml", 
				new TaskCreateViewModel()
				{
					CourseId = courseId.Value,
					Course = course
				});
	    }

        [Authorize]
		public async Task<ActionResult> SingInCourse(long courseId)
        {
            var course = EduRepository.CourseManager.Get(c => c.Id == courseId);
            var user = EduRepository.UserManager.Get(u => u.Email == User.Identity.Name);

	        if (!EduRepository.AddCourseMate(course, user))
		        ModelState.AddModelError("", "Ошибка при обновлении базы данных");
	        else
	        {
		        await NotificationsService.SendNotifications(u => u.Email == course.MentorsEmail,
													  u => $"Пользователь {u.Email} вступил в курс {course.Name}");
	        }
            return View("Index", course);
        }
    }
}