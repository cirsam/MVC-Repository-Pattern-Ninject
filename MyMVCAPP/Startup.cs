using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyMVCAPP.Startup))]
namespace MyMVCAPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
