using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_5032_MVC_News.Startup))]
namespace _5032_MVC_News
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
