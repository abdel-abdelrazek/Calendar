using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Calendar.Startup))]
namespace Calendar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
