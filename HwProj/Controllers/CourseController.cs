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
		    var cource = (Course) courseView;
		    cource.MentorName = User.Identity.Name + " " + User.Identity.GetUserSurname();
			if (EduRepository.CourseManager.Contains(t => Course.EqualDescription(t, cource)))
			    ModelState.AddModelError("", "Курс с таким описанием уже существует");
		    else
		    {
			    if (EduRepository.CourseManager.Add(cource)) ; //успех
				else ModelState.AddModelError("", "Ошибка при созданни курса. Повторите попытку.");
			}
		    return View();
	    }
    }
}