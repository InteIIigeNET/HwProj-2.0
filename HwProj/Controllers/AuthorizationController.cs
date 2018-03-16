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
			    User user;
			    using (var db = new AuthContext())
			    {
				    user = db.Users.FirstOrDefault(
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
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (var db = new AuthContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (var db = new AuthContext())
                    {
                        db.Users.Add(new User
                        {
                            Id = Guid.NewGuid(),
                            Name = model.Name,
                            Surname = model.Surname,
                            CreatedAt = DateTime.Now,
                            Gender = model.Gender,
                            Email = model.Email,
                            EncryptedPassword = model.Password
                        });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == model.Email && u.EncryptedPassword == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        public ActionResult LogOut()
	    {
		    FormsAuthentication.SignOut();
		    return RedirectToAction("Index", "Home");
	    }
	}
}