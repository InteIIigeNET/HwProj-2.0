using System;
using HwProj.Models.Contexts;
using Microsoft.Ajax.Utilities;

namespace HwProj.Models.Repositories
{
	internal class MainRepository : IDisposable
    {
        public CoursesManager           CourseManager               { get; }
        public HomeworksManager         HomeworkManager             { get; }
        public UserManager              UserManager                 { get; }
        public TasksManager             TaskManager                 { get; }
        public NotificationsManager     NotificationsManager        { get; }
	    public CourseMateManager        CourseMateManager           { get; }
        public PullRequestsDataManager  PullRequestsDataManager     { get; }

		#region Singleton
		private static readonly Lazy<MainRepository> Lazy =
        new Lazy<MainRepository>(() => new MainRepository());

        public static MainRepository Instance => Lazy.Value;

	    #endregion

        private MainRepository()
        {
	        _context                    = this.GetContext();

            CourseManager               = new CoursesManager            (_context);
            UserManager                 = new UserManager               (_context);
            HomeworkManager             = new HomeworksManager          (_context);
            TaskManager                 = new TasksManager              (_context);
            NotificationsManager        = new NotificationsManager      (_context);
	        CourseMateManager           = new CourseMateManager         (_context);
            PullRequestsDataManager     = new PullRequestsDataManager   (_context);
        }

	    private readonly AppDbContext _context;
        public void Dispose()
        {
	        _context.IfNotNull(c => c.Dispose());
            GC.SuppressFinalize(this);
        }
    }
}