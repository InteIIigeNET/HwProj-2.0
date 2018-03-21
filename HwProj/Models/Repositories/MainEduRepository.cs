using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HwProj.Models;
using HwProj.Models.Enums;
using HwProj.Models.ViewModels;
using HwProj.Models.Contexts;

namespace HwProj.Models.Repositories
{
    public class MainEduRepository : IDisposable
    {
        public IRepository<Course> CourseManager { get; }
        public IRepository<Homework> HomeworkManager { get; }


        #region Singleton
        private static readonly Lazy<MainEduRepository> lazy =
        new Lazy<MainEduRepository>(() => new MainEduRepository());

        public static MainEduRepository Instance { get { return lazy.Value; } }
        #endregion

        private ApplicationDbContext context;
        private MainEduRepository()
        {
            context = new ApplicationDbContext();
            CourseManager = new CoursesManager(context);
            //HomeworkManager = new HomeworksManager(context);
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}