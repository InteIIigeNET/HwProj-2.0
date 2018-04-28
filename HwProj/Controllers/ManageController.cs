using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using HwProj.Models;
using HwProj.Models.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using HwProj.Filters;
using static HwProj.Controllers.AccountController;
using System.Security.Claims;
using HwProj.Tools;
using Microsoft.Ajax.Utilities;
using HwProj.GitHubService;

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
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
	        private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
	        private set => _userManager = value;
        }

		//
		// GET: /Manage/Index
		public ActionResult Index()
		{
            return View();
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
		    else ModelState.AddModelError("", "Введён неверный пароль");
		    return View(model);
		}


		public ActionResult Delete()
	    {
		    return View();
	    }

		//[HttpDelete]
		public async Task<ActionResult> ConfirmRemove()
	    {
		    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
		    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			await UserManager.DeleteAsync(user);
			return RedirectToAction("Index", "Home");
		}

		public ActionResult Back()
	    {
		    return RedirectToAction("Index", "Manage");
	    }

        #region PartialViewAction
        
        public ActionResult _EditProfileInfoPartial()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return PartialView(new EditViewModel(user));           
        }

        public async Task<ActionResult> _EditProfileSocialPartial()
        {
            var user = await UserManager.FindByEmailAsync(User.Identity.Name);

            var allAuthProvider = from p in HttpContext.GetOwinContext().Authentication.GetExternalAuthenticationTypes()
                                  select p.AuthenticationType;


            return PartialView(new ExternalLoginListViewModel
            {
                ReturnUrl = Request.Url.AbsoluteUri,
                LoginProviders = allAuthProvider.Except(
                    from p in user.Logins
                    select p.LoginProvider)
            });
        } 
        #endregion



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