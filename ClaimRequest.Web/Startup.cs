using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(ClaimRequest.Web.Startup))]
namespace ClaimRequest.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
