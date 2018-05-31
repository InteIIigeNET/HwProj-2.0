using System.Web.Mvc;
using HwProj.Models.Repositories;

namespace HwProj.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        MainRepository _db = MainRepository.Instance;

        public ActionResult Index()
        {
			if (User.Identity.IsAuthenticated)
            {
                var user = _db.UserManager.Get(u => u.Email == User.Identity.Name);
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
    }
}