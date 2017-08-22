using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TodoWebApp.Startup))]
namespace TodoWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
