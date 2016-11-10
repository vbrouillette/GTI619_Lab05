using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GTI619_Lab5.Startup))]
namespace GTI619_Lab5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
