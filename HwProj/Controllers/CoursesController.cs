using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
		private readonly MainRepository _repository = MainRepository.Instance;

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
				MentorId = User.Identity.GetUserId(),
				Mentor = _repository.UserManager.Get(u => u.Id == User.Identity.GetUserEmail()),
			};

			if (_repository.CourseManager.Contains(t => t.CompareTo(course) == 0))
				ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
			else
			{
				if (_repository.CourseManager.Add(course))
					return RedirectToAction("Index", new { courseId = course.Id });

				else ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
			}
			return View();
		}

		[AllowAnonymous]
		public ActionResult Index(long? courseId)
		{
			return courseId.HasValue ?
				  View(_repository.CourseManager.Get(c => c.Id == courseId)) 
				: View("CoursesList", _repository.CourseManager.GetAll());
		}

		[AllowAnonymous]
		public ActionResult FindCourses(string pattern)
		{
			return PartialView("CoursesList", Find(pattern));
			/* неоптимально */
		}

		[AllowAnonymous]
		public JsonResult GetSearchResult(string search)
		{
			var data = Find(search).Select(c => new CourseSearchViewModel(c));
			return new JsonResult {Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
		}

		private IEnumerable<Course> Find(string search)
		{
			var data = String.IsNullOrEmpty(search)
				? _repository.CourseManager.GetAll()
				: _repository.CourseManager.GetAll(c => c.Name.Contains(search)
				                                        || c.Mentor.Email.Contains(search)
				                                        || c.Mentor.UserName.Contains(search));
			return data;
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

			var course = _repository.CourseManager.Get(t => t.MentorId == User.Identity.GetUserId());
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
			var course = _repository.CourseManager.Get(c => c.Id == courseId);
			var user = _repository.UserManager.Get(u => u.Email == User.Identity.Name);

			if (!_repository.CourseMateManager.Add((course, user)))
		        ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
	        else
	        {
				await NotificationsService.SendNotifications(new [] {course.Mentor},
					  u => $"Пользователь {u.Email} вступил в курс {course.Name}" +
					  (course.IsOpen? "" : new Button(Request.RequestContext, "Принять", "AcceptUser", "Courses", 
										   new { courseId = courseId, userId = user.Id, notifyId = Notification.ContextId}) +
										   new Button(Request.RequestContext, "Отклонить", "RejectUser", "Courses",
										   new { courseId = courseId, userId = user.Id, notifyId = Notification.ContextId})));
	        }
            return View("Index", course);
        }

		[Authorize(Roles = "Преподаватель")]
		public async Task<ActionResult> AcceptUser(long courseId, string userId, long? notifyId)
		{
			var course = _repository.CourseManager.Get(c => c.Id == courseId);
			if (course.MentorId != User.Identity.GetUserId())
			{
				/* Если это не ментор, не показываем этого */
				return RedirectToAction("Index", "Home");
			}
			var user = _repository.UserManager.Get(u => u.Id == userId);

			if (!_repository.CourseMateManager.Accept((course, user)))
				ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
			else
			{
				await NotificationsService.SendNotifications(new[] { user },
					u => $"Ваша заявка на курс <b>{course.Name}</b> была принята преподавателем");
				if (notifyId.HasValue) _repository.NotificationsManager.Delete(notifyId.Value);
			}
			return View("Index", course);
		}

		[Authorize(Roles = "Преподаватель")]
		public async Task<ActionResult> RejectUser(long courseId, string userId, long? notifyId)
		{
			var course = _repository.CourseManager.Get(c => c.Id == courseId);

			if (course.MentorId != User.Identity.GetUserId())
			{
				/* Если это не ментор, то не показываем */
				return RedirectToAction("Index", "Home");
			}
			var user = _repository.UserManager.Get(u => u.Id == userId);

			if (!_repository.CourseMateManager.Delete((course, user)))
			{
				ModelState.AddModelError("", @"Ошибка при обновлении базы данных");
			}
			else
			{
				await NotificationsService.SendNotifications(new[] {user},
					u => $"Ваша заявка на курс <b>{course.Name}</b> была отклонена преподавателем");
				if (notifyId.HasValue) _repository.NotificationsManager.Delete(notifyId.Value);
			}
			return RedirectToAction("Index", "Home");
		}
	}
}