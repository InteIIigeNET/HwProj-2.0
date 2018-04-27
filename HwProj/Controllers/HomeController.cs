using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using HwProj.Models.Repositories;
using HwProj.Services;
using HwProj.Tools;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HwProj.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        MainEduRepository Db = MainEduRepository.Instance;

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = Db.UserManager.Get(u => u.Email == User.Identity.Name);
				return View(user);
            }
            var courses = Db.CourseManager.GetAll();
            return View(courses);
        }
    }
}