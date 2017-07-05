using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(STEnterprise.Startup))]
namespace STEnterprise
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
