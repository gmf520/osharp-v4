using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RK.TZ.Web.Startup))]
namespace RK.TZ.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
