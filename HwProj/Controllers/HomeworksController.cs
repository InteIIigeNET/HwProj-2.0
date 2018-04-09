//using HwProj.Models;
//using HwProj.Models.Repositories;
//using HwProj.Models.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace HwProj.Controllers
//{
//    public class HomeworksController : Controller
//    {
//        private MainEduRepository EduRepository = MainEduRepository.Instance;

//        [Authorize]
//        public ActionResult Index(Homework homework)
//        {
//            return View(homework);
//        }

//        public ActionResult Create()
//        {
//            return View();
//        }

//        [Authorize]
//        [HttpPost]
//        public ActionResult Create(HomeworkCreateViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                ModelState.AddModelError("", "Нужно заполнить все поля");
//                return View(model);
//            }
//            EduRepository.HomeworkManager.Add(new Homework(model) { Task = EduRepository.TaskManager.Get(t => t.Id == model.TaskId)});
//            ModelState.AddModelError("", "Дз успешно добавлено");
//            return View();
//        }
//    }
//}