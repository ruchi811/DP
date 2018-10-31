using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DPWebApplication.Startup))]
namespace DPWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
