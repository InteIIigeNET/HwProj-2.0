using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HwProj.Models;
using HwProj.Models.ManagerModels;

namespace HwProj.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

		//
		// GET: /Manage/Index
		public ActionResult Index()
		{
			var user = UserManager.FindById(User.Identity.GetUserId());
			return View((EditViewModel)user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Index(EditViewModel model)
	    {
		    if (!ModelState.IsValid)
		    {
			    return View(model);
		    }
		    var user = UserManager.FindById(User.Identity.GetUserId());
		    if (await UserManager.CheckPasswordAsync(user, model.Password))
		    {
			    user.EditFrom(model);
			    var result = await UserManager.UpdateAsync(user);

			    if (result.Succeeded)
			    {
				    ViewBag.Title = "Данные успешно обновлены";
			    }
			    else
			    {
				    AddErrors(result);
			    }
		    }
		    else
		    {
			    ModelState.AddModelError("", "Введён неверный пароль");
		    }
		    return View(model);
		}


		public async Task<ActionResult> Delete()
	    {
		    return View();
	    }

		[HttpDelete]
		public async Task<ActionResult> ConfirmRemove()
	    {
		    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
		    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			await UserManager.DeleteAsync(user);
			return RedirectToAction("Index", "Home");
		}

		public async Task<ActionResult> Back()
	    {
		    return RedirectToAction("Index", "Manage");
	    }

		////
		//// GET: /Manage/SetPassword
		//public ActionResult SetPassword()
		//{
		//    return View();
		//}

		////
		//// POST: /Manage/SetPassword
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
		//        if (result.Succeeded)
		//        {
		//            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
		//            if (user != null)
		//            {
		//                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
		//            }
		//            return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
		//        }
		//        AddErrors(result);
		//    }

		//    // Это сообщение означает наличие ошибки; повторное отображение формы
		//    return View(model);
		//}

		////
		//// GET: /Manage/ManageLogins
		//public async Task<ActionResult> ManageLogins(ManageMessageId? message)
		//{
		//    ViewBag.StatusMessage =
		//        message == ManageMessageId.RemoveLoginSuccess ? "Внешнее имя входа удалено."
		//        : message == ManageMessageId.Error ? "Произошла ошибка."
		//        : "";
		//    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
		//    if (user == null)
		//    {
		//        return View("Error");
		//    }
		//    var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
		//    var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
		//    ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
		//    return View(new ManageLoginsViewModel
		//    {
		//        CurrentLogins = userLogins,
		//        OtherLogins = otherLogins
		//    });
		//}

		////
		//// POST: /Manage/LinkLogin
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult LinkLogin(string provider)
		//{
		//    // Запрос перенаправления к внешнему поставщику входа для связывания имени входа текущего пользователя
		//    return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
		//}

		////
		//// GET: /Manage/LinkLoginCallback
		//public async Task<ActionResult> LinkLoginCallback()
		//{
		//    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
		//    if (loginInfo == null)
		//    {
		//        return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
		//    }
		//    var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
		//    return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
		//}

		protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

#endregion
    }
}