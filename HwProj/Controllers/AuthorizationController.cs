/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HwProj.Models;
using HwProj.Models.Contexts;
using HwProj.Models.ManagerModels;
using HwProj.Repository;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace HwProj.Controllers
{
    public class AuthorizationController : Controller
    {
	    public ActionResult LogIn()
	    {
		    return View();
	    }

		[HttpPost]
	    [ValidateAntiForgeryToken]
	    public ActionResult LogIn(LoginModel model)
	    {
		    if (ModelState.IsValid)
		    {
			    User user = null;
			    using (var db = UserRepository.Instance)
			    {
				    user = db.Get(
						u => u.Email == model.Email && u.EncryptedPassword == model.Password);
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ViewModels viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (var db = UserRepository.Instance)
                {
                    user = db.Get(u => u.Email == viewModel.Email);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (var db = UserRepository.Instance)
                    {
                        if (db.Add(new User
                        {
                            Id = Guid.NewGuid(),
                            Name = viewModel.Name,
                            Surname = viewModel.Surname,
                            CreatedAt = DateTime.Now,
                            Gender = viewModel.Gender,
                            Email = viewModel.Email,
                            EncryptedPassword = viewModel.Password
                        }))
                        {
                            FormsAuthentication.SetAuthCookie(viewModel.Name, true);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                            ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    }
                }
            }

            return View(viewModel);
        }

        public ActionResult LogOut()
	    {
		    FormsAuthentication.SignOut();
		    return RedirectToAction("Index", "Home");
	    }
	}
}*/