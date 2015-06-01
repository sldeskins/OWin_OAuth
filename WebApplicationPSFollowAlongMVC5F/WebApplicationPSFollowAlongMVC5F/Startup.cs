using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_WebApplication1.Startup))]
namespace _WebApplication1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
