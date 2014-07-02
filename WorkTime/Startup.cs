using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkTime.Startup))]
namespace WorkTime
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
