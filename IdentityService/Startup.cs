using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityService.Startup))]
namespace IdentityService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
