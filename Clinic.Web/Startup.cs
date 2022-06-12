using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Clinic.Web.Startup))]
namespace Clinic.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
