using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Identify.Startup))]
namespace Identify
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
