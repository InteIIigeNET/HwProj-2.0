using HwProj.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using HwProj.Models.ViewModels;
using HwProj.Models.Repositories;
using HwProj.Services;
using HwProj.Tools;
using Microsoft.AspNet.Identity;

namespace HwProj.Controllers
{
	[Authorize]
	public class CoursesController : Controller
	{
		private readonly MainEduRepository _eduRepository = MainEduRepository.Instance;

		[Authorize(Roles = "Преподаватель")]
		public ActionResult Create()
		{
			return View();
		}

		[Authorize(Roles = "Преподаватель")]
		[HttpPost]
		public ActionResult Create(CreateCourseViewModel courseView)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var course = new Course(courseView)
			{
				MentorsName = User.Identity.GetUserFullName(),
				MentorsEmail = User.Identity.GetUserEmail(),
			};

			if (_eduRepository.CourseManager.Contains(t => t.CompareTo(course) == 0))
				ModelState.AddModelError("", "Курс с таким описанием уже существует");
			else
			{
				if (_eduRepository.CourseManager.Add(course))
					return RedirectToAction("Index", new { courseId = course.Id });

				else ModelState.AddModelError("", "Ошибка при созданни курса. Повторите попытку позже.");
			}
			return View();
		}

		[AllowAnonymous]
		public ActionResult Index(long? courseId)
		{
			return courseId.HasValue ?
				  View(_eduRepository.CourseManager.Get(c => c.Id == courseId)) 
				: View("CoursesList", _eduRepository.CourseManager.GetAll());
		}

		[Authorize(Roles = "Преподаватель")]
		public ActionResult Edit()
		{
			return null;
		}

		[Authorize(Roles = "Преподаватель")]
		public ActionResult AddTask(long? courseId)
		{
			if (courseId == null) return View("CoursesList");

			var course = _eduRepository.CourseManager.Get(t => t.MentorsEmail == User.Identity.Name);
			if (course == null) return View("CoursesList");
			return View("~/Views/Tasks/Create.cshtml",
				new TaskCreateViewModel()
				{
					CourseId = courseId.Value,
					Course = course
				});
		}

		[Authorize]
		public async Task<ActionResult> SingInCourse(long courseId)
		{
			var course = _eduRepository.CourseManager.Get(c => c.Id == courseId);
			var user = _eduRepository.UserManager.Get(u => u.Email == User.Identity.Name);

			if (!_eduRepository.CourseMateManager.Add((course, user)))
		        ModelState.AddModelError("", "Ошибка при обновлении базы данных");
	        else
	        {
				await NotificationsService.SendNotifications(u => u.Email == course.MentorsEmail,
										   u => $"Пользователь {u.Email} вступил в курс {course.Name}" +
											      (course.IsOpen? "" : new Button(Request.RequestContext, "Принять", "AcceptUser", "Courses", 
																	   new { courseId = courseId, userId = user.Id}) +
																       new Button(Request.RequestContext, "Отклонить", "RejectUser", "Courses",
												                       new { courseId = courseId, userId = user.Id })));
	        }
            return View("Index", course);
        }

		[Authorize(Roles = "Преподаватель")]
		public ActionResult AcceptUser(long courseId, string userId)
		{
			var course = _eduRepository.CourseManager.Get(c => c.Id == courseId);
			if (course.MentorsEmail != User.Identity.Name)
			{
				/* Если это не ментор */
				return RedirectToAction("Index", "Home");
			}

			var user = _eduRepository.UserManager.Get(u => u.Id == userId);

			if (!_eduRepository.CourseMateManager.Accept((course, user)))
				ModelState.AddModelError("", "Ошибка при обновлении базы данных");
			return View("Index", course);
		}

		[Authorize(Roles = "Преподаватель")]
		public async Task<ActionResult> RejectUser(long courseId, string userId)
		{
			var course = _eduRepository.CourseManager.Get(c => c.Id == courseId);

			if (course.MentorsEmail != User.Identity.Name)
			{
				/* Если это не ментор */
				return RedirectToAction("Index", "Home");
			}
			var user = _eduRepository.UserManager.Get(u => u.Id == userId);

			if (!_eduRepository.CourseMateManager.Delete((course, user)))
			{
				ModelState.AddModelError("", "Ошибка при обновлении базы данных");
			}
			else
			{
				await NotificationsService.SendNotifications(new[] {user},
					u => $"Ваша заявка на курс <b>{course.Name}</b> была отклонена преподавателем");
			}

			return RedirectToAction("Index", "Home");
		}
	}
}