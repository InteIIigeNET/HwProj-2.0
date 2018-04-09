﻿using System;
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
using System.Data.Entity.Infrastructure;

namespace HwProj.Models.Repositories
{
    public class MainEduRepository : IDisposable
    {
        public CoursesManager CourseManager { get; }
        public HomeworksManager HomeworkManager { get; }
        public UserManager UserManager { get; }
        public TasksManager TaskManager { get; }
        public NotificationsManager NotificationsManager { get; }
	    public CourseMateManager CourseMateManager { get; }

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
            UserManager = new UserManager(context);
            HomeworkManager = new HomeworksManager(context);
            TaskManager = new TasksManager(context);
            NotificationsManager = new NotificationsManager(context);
	        CourseMateManager = new CourseMateManager(context);
		}

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}