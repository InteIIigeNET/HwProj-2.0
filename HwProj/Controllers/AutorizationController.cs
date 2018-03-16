using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HwProj.Models;
using HwProj.Models.Contexts;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace HwProj.Controllers
{
    public class AutorizationController : Controller
    {
	    [HttpPost]
	    [ValidateAntiForgeryToken]
	    public ActionResult Login(LoginModel model)
	    {
		    if (ModelState.IsValid)
		    {
			    User user;
			    using (EduContext db = new EduContext())
			    {
				    user = db.Users.FirstOrDefault(
						u => u.Email == model.Email && u. == model.Password);

			    }
			    if (user != null)
			    {
				    FormsAuthentication.SetAuthCookie(model.Email, true);
				    return RedirectToAction("Index", "Home");
			    }
			    else
			    {
				    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
			    }
		    }

		    return View(model);
	    }
	}
}