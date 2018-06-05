using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HwProj.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using HwProj.Models;
using HwProj.Models.Enums;
using HwProj.Models.ViewModels;
using HwProj.Services.NotificationPatterns;
using HwProj.Tools;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.Owin;

namespace HwProj.Controllers
{
	[RequireHttps]
	[Authorize]
    public partial class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        { }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
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

        // GET: /Account/Login
		[AnonymousOnly]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
                    return View(model);
            }
        }

		// GET: /Account/Register
		[AllowAnonymous]
		[AnonymousOnly]
		public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
	            if (!String.IsNullOrEmpty(model.InvitedBy))
	            {
		            return await RegisterByInvite(model);
	            }
	            return await RegisterByYourSelf(model);
            }
            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }

	    private async Task<ActionResult> RegisterByInvite(RegisterViewModel model)
	    {
		    var isDone = await UserManager.VerifyUserTokenAsync(model.InvitedBy, model.Email + "_invite",
			    token: model.InviteToken);
		    if (isDone)
		    {
			    var user = new User(model);
			    var result = await UserManager.CreateAsync(user, model.Password);
			    if (result.Succeeded)
			    {
				    await UserManager.AddToRoleAsync(user.Id, RoleType.Преподаватель.ToString());
				    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
				    await (new UserAcceptedInviteNotification(model.InvitedBy, user)).Send();
				    return RedirectToAction("Index", "Home");
			    }
			    AddErrors(result);
			    return View("Register");
		    }
		    else
			    return RedirectToAction("Index", "Home",
				    new {errorMessage = @"Срок действия пригласительной ссылки закончился"});
	    }

	    private async Task<ActionResult> RegisterByYourSelf(RegisterViewModel model)
	    {
		    var user = new User(model);
			var result = await UserManager.CreateAsync(user, model.Password);
		    if (result.Succeeded)
		    {
			    await UserManager.AddToRoleAsync(user.Id, RoleType.Студент.ToString());
			    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

			    // Отправка сообщения электронной почты для подтверждения
			    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
			    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
			    await UserManager.SendEmailAsync(user.Id, "Подтверждение учетной записи HwProj", "Подтвердите вашу учетную запись, перейдя по <a href=\"" + callbackUrl + "\">ссылке</a>");

			    return RedirectToAction("Index", "Home");
		    }
		    AddErrors(result);
		    return View("Register");
	    }

		[AllowAnonymous]
	    public ActionResult AcceptInvite(string invitedById, string email, string code, bool isTeacher)
	    {
		    return View("Register", new RegisterViewModel() {Email = email, InvitedBy = invitedById, InviteToken = code});
	    }

	    // GET: /Account/ConfirmEmail
		[AllowAnonymous]
		public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
		    var result = await UserManager.ConfirmEmailAsync(userId, code);
			return result == null ? View("Error") 
								  : View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

		// GET: /Account/ForgotPassword
		[AllowAnonymous]
		[AnonymousOnly]
		public ActionResult ForgotPassword()
		{
			return View();
		}

		// POST: /Account/ForgotPassword
		[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Не показывать, что пользователь не существует или не подтвержден
                    return View("ForgotPasswordConfirmation");
                }

                 string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                 var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                 await UserManager.SendEmailAsync(user.Id, "Сброс пароля", "Сбросьте ваш пароль, щелкнув <a href=\"" + callbackUrl + "\">здесь</a>");
                 return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        [AnonymousOnly]
		public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Не показывать, что пользователь не существует
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
		[ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Запрос перенаправления к внешнему поставщику входа
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
		public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            // Выполнение входа пользователя посредством данного внешнего поставщика входа, если у пользователя уже есть имя входа
	        var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
			switch (result)
            {
                case SignInStatus.Success:
	                var user = UserManager.FindByEmail(loginInfo.Email);
					if (loginInfo.Login.LoginProvider == "GitHub")
						await AddGitHubToken((await UserManager.FindByEmailAsync(loginInfo.Email)).Id);
					return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // Если у пользователя нет учетной записи, то ему предлагается создать ее
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
			        return View("ExternalLoginConfirmation",
				            new ExternalLoginConfirmationViewModel { Email = loginInfo.Email});
			}
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // Получение сведений о пользователе от внешнего поставщика входа
	            var info = await AuthenticationManager.GetExternalLoginInfoAsync();
				if (info == null) return View("ExternalLoginFailure");
	            try
	            {
		            User user = await UserManager.FindByEmailAsync(model.Email);
					var result = IdentityResult.Success;
		            if (user == null)
		            {
			            user = new User(model);
			            await UserManager.CreateAsync(user);
			            result = await UserManager.AddToRoleAsync(user.Id, RoleType.Преподаватель.ToString());
		            }
		            if (result.Succeeded)
		            {
			            if (info.Login.LoginProvider == "GitHub") await AddGitHubToken(user.Id);

			            result = await UserManager.AddLoginAsync(user.Id, info.Login);
			            if (result.Succeeded)
			            {
				            if(!User.Identity.IsAuthenticated)
								await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
				            return RedirectToLocal(returnUrl);
			            }
		            }
		            AddErrors(result);
	            }
				catch { return View("ExternalLoginFailure"); }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

	    private async System.Threading.Tasks.Task AddGitHubToken(string userId)
	    {
		    async Task<Claim> GetGitHubToken()
		    {
			    var claimsIdentity =
				    await AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
			    var gitHubAccessTokenClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type.Equals("GitHubAccessToken"));
			    return gitHubAccessTokenClaim;
		    }

		    var gitClaim = await GetGitHubToken();
		    var currentClaim =
			    (await UserManager.GetClaimsAsync(userId)).FirstOrDefault(c => c.Type.Equals("GitHubAccessToken"));

		    await currentClaim.IfNotNull(async c => await UserManager.RemoveClaimAsync(userId, c));
		    await UserManager.AddClaimAsync(userId, gitClaim);
	    }

	    // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

	    private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}