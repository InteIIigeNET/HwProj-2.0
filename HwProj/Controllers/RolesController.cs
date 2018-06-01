using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HwProj.Models.Enums;
using HwProj.Models.Repositories;
using HwProj.Models.Roles;
using HwProj.Models.ViewModels;
using HwProj.Services.NotificationPatterns;
using HwProj.Tools;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HwProj.Controllers
{
	[Authorize]
	public class RolesController : Controller
	{
		private ApplicationUserManager _userManager;
		public RolesController()
		{ }
		public RolesController(ApplicationUserManager userManager)
		{
			UserManager = userManager;
		}
		public ApplicationUserManager UserManager
		{
			get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			private set => _userManager = value;
		}

		[Authorize(Roles = "Преподаватель")]
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Преподаватель")]
		public async Task<ActionResult> Index(string email)
		{
			if (!String.IsNullOrEmpty(email))
			{
				var addedTeacher = await UserManager.FindByEmailAsync(email);
				var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

				var result = await UserManager.RemoveFromRoleAsync(addedTeacher.Id, RoleType.Студент.ToString());
				if (result.Succeeded)
				{
					result = await UserManager.AddToRoleAsync(addedTeacher.Id, RoleType.Преподаватель.ToString());
					if (result.Succeeded)
					{
						await (new TeacherAddedNotification(currentUser, addedTeacher, Request)).Send();
						this.AddViewBagMessage("Отличные новости: +1 преподаватель в сообщество HwProj");
					}
				}
				else this.AddViewBagMessage("Плохие новости: не удалось добавить преподавателя");
			}
			return View();
		}

		[Authorize(Roles = "Преподаватель")]
		[HttpGet]
		public JsonResult GetSearchResult(string search)
		{
			var users = UserManager.Users.Where(u => u.Roles.Any(r => r.RoleId == "2")).ToList();
			var data = String.IsNullOrEmpty(search)
					? users.Select(u => new UserSearchViewModel(u))
					: users.Where(u => u.Name.Contains(search)
								   || u.Email.Contains(search)
					               || u.Surname.Contains(search))
					               .Select(u => new UserSearchViewModel(u));

			return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
		}

		public ActionResult Invite()
		{
			return View(new UserInviteViewModel(isTeacher: true));
		}

		[HttpPost]
		[Authorize(Roles = "Преподаватель")]
		public async Task<ActionResult> Invite(UserInviteViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (await UserManager.FindByEmailAsync(model.Email) == null)
				{
					var sender = await UserManager.FindByIdAsync(User.Identity.GetUserId());

					string code = await UserManager.GenerateUserTokenAsync($"{model.Email}_invite", sender.Id);
					string url = Url.Action("AcceptInvite", "Account", new { invitedById = sender.Id,
																			       email = model.Email,
																					code = code,
																			   isTeacher = true }, protocol: Request.Url.Scheme);

					await UserManager.EmailService.SendAsync(
						new IdentityMessage()
						{
							Destination = model.Email,
							Body = $"Пользователь <b>{sender.Name} {sender.Surname}</b> (" +
							       (new MailTo(sender.Email, "Ответ на приглашение в команду HwProj")) +
							       ") приглашает вас в команду HwProj:<br><br>\"" + model.Message + 
								   $"\"<br><br>Перейдите по <a href={url}>ссылке</a>, чтобы подтвердить регистрацию.",
							Subject = "Приглашение в команду HwProj"
						});
					this.AddViewBagMessage("Приглашение было успешно отправлено");
				}
				else this.AddViewBagError("Пользователь уже зарегистрирован на HwProj");
			}
			return View();
		}
	}
}