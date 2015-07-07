using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiPSServerSample.Startup))]
namespace DiPSServerSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
