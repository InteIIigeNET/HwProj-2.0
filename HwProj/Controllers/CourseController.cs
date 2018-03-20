using HwProj.Models;
using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HwProj.Models.Repositories;

namespace HwProj.Controllers
{
	[Authorize]
    public class CourseController : Controller
    {
        private MainEduRepository EduRepository = MainEduRepository.Instance;
        public ActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            return View(course);
        }
    }
}