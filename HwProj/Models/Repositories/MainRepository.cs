using System;
using HwProj.Models.Contexts;
using Microsoft.Ajax.Utilities;

namespace HwProj.Models.Repositories
{
	internal class MainRepository : IDisposable
    {
        public CoursesManager       CourseManager        { get; }
        public HomeworksManager     HomeworkManager      { get; }
        public UserManager          UserManager          { get; }
        public TasksManager         TaskManager          { get; }
        public NotificationsManager NotificationsManager { get; }
	    public CourseMateManager    CourseMateManager    { get; }

		#region Singleton
		private static readonly Lazy<MainRepository> lazy =
        new Lazy<MainRepository>(() => new MainRepository());

        public static MainRepository Instance => lazy.Value;

	    #endregion

        private MainRepository()
        {
            CourseManager        = new CoursesManager(Context);
            UserManager          = new UserManager(Context);
            HomeworkManager      = new HomeworksManager(Context);
            TaskManager          = new TasksManager(Context);
            NotificationsManager = new NotificationsManager(Context);
	        CourseMateManager    = new CourseMateManager(Context);
		}

	    private AppDbContext Context = AppDbContext.Create();
        public void Dispose()
        {
	        Context.IfNotNull(c => c.Dispose());
            GC.SuppressFinalize(this);
        }
    }
}