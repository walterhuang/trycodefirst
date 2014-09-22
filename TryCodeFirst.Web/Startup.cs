using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TryCodeFirst.Web.Startup))]
namespace TryCodeFirst.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
