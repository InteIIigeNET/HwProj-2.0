using System;
using HwProj.Models.Contexts;

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
            CourseManager        = new CoursesManager();
            UserManager          = new UserManager();
            HomeworkManager      = new HomeworksManager();
            TaskManager          = new TasksManager();
            NotificationsManager = new NotificationsManager();
	        CourseMateManager    = new CourseMateManager();
		}

        public void Dispose()
        {
			/* Hmmm */
            GC.SuppressFinalize(this);
        }
    }
}