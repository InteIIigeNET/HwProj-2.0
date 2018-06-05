using System;
using System.Web.Mvc;
using HwProj.Models.Repositories;
using HwProj.Tools;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace HwProj.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        MainRepository _db = MainRepository.Instance;

        public ActionResult Index(string errorMessage = "")
        {
	        if (!String.IsNullOrEmpty(errorMessage))
	        {
		        this.AddViewBagError(errorMessage);
	        }
			if (User.Identity.IsAuthenticated)
            {
                var user = _db.UserManager.Get(u => u.Id == User.Identity.GetUserId());
                return View(user);
            }
            var courses = _db.CourseManager.GetAll();
            return View(courses);
        }
		[Authorize]
        public void ReadNotification(long? id)
        {
	        if (id.HasValue)
	        {
		        _db.NotificationsManager.Update(n => n.Id == id, n => n.IsRead = true);
	        }
        }

        [Authorize]
        public void ReadAllNotifications()
        {
            _db.NotificationsManager.GetAll()
                .Where(n => n.UserId == User.Identity.GetUserId())
                .ToList()
                .ForEach(n => ReadNotification(n.Id));
        }

        [Authorize]
        public ActionResult GetUnreadNotificationCount()
        {
            var user = _db.UserManager.Get(u => u.Id == User.Identity.GetUserId());
            var count = user.NoReadNotifications.Count();
            return Json(count, JsonRequestBehavior.AllowGet);
        }
    }
}