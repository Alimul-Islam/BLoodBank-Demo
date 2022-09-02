using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BB2.Startup))]
namespace BB2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
