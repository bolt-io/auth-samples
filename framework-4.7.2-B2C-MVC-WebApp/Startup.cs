using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(framework_4._7._2_B2C_MVC_WebApp.Startup))]
namespace framework_4._7._2_B2C_MVC_WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
