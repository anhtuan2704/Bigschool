using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab04.Startup))]
namespace lab04
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
