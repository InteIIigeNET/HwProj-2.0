using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HwProj.Models;
using HwProj.Models.Contexts;
using HwProj.Repository;

namespace HwProj.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Edit()
        {
	        if (!User.Identity.IsAuthenticated) 
		        return View("Error", new Error(){Message = "Ошибка авторизации."});

			User dbUser = UserRepository.Instance.Get(u => u.Email == User.Identity.Name);
	        if (dbUser != null)
	        {
		        return View(dbUser);
	        }
	        else
	        {
		        return View("Error", new Error() { Message = $"Пользователь {User.Identity.Name} не найден." });
			}
        }

		[HttpPut]
	    public ActionResult Edit(User user)
	    {
		    if (User.Identity.IsAuthenticated)
		    {
			    User dbUser = UserRepository.Instance.Get(u => u.Email == User.Identity.Name);
			    if (dbUser != null)
			    {
				    if (true)//user.EncryptedPassword == dbUser.EncryptedPassword)
				    {
					    UserRepository.Instance.Update(user);
				    }
				    else
				    {
						return View("Error", new Error() {Message = "Неверные данные авторизации."});
					}
			    }
			    else
			    {
				    return View("Error", new Error() { Message = $"Пользователь {User.Identity.Name} не найден." });
				}
		    }
		    else
		    {
			    return View("Error", new Error() { Message = $"Ошибка авторизации." });
			}
		    ModelState.AddModelError("", "Данные успешно обновлены.");
			return View(user);
	    }
	}
}