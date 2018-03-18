using System.Data.Entity;
using HwProj.Models.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace HwProj.Models.ManagerModels
{
	public class ApplicationManager : UserManager<User>
	{
		public ApplicationManager(IUserStore<User> store) : base(store)
		{
		}
		public static ApplicationManager Create(IdentityFactoryOptions<ApplicationManager> options,
			IOwinContext context)
		{
			ApplicationDbContext db = context.Get<ApplicationDbContext>();
			ApplicationManager manager = new ApplicationManager(new UserStore<User>(db));
			return manager;
		}
	}
}