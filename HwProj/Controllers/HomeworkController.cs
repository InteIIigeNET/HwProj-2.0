using HwProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HwProj.Controllers
{
    public class HomeworkController : Controller
    {
        
        public ActionResult Index(Homework homework)
        {
            return View(homework);
        }
    }
}