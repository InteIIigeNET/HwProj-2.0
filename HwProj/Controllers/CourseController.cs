using HwProj.Models;
using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HwProj.Controllers
{
    public class CourseController : Controller
    {

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