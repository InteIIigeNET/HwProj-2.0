using HwProj.Models;
using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using HwProj.Models.Enums;
using HwProj.Models.ViewModels;
using HwProj.Models.Repositories;
using HwProj.Tools;
using Microsoft.AspNet.Identity;

namespace HwProj.Controllers
{
	[Authorize]
    public class CourseController : Controller
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
			    if (EduRepository.CourseManager.Add(course)) ; //успех
				else ModelState.AddModelError("", "Ошибка при созданни курса. Повторите попытку.");
			}
		    return View();
	    }

        [AllowAnonymous]
	    public ActionResult Index(Guid courseId)
        {
            return View(EduRepository.CourseManager.Get(c => c.Id == courseId));
        }
    }
}