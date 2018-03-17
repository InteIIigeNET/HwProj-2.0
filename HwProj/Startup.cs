using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HwProj.Startup))]
namespace HwProj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
