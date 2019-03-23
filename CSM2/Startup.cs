using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSM2.Startup))]
namespace CSM2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
