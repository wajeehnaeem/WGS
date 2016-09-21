using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using WGS;
using WGS.Models;
using WGS.Security;

[assembly: OwinStartup(typeof(Startup))]

namespace WGS
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            CookieAuthenticationOptions options = new CookieAuthenticationOptions() {AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Auth/Login"), LogoutPath = new PathString("/Home/Index")};
            app.UseCookieAuthentication(options);
            app.CreatePerOwinContext(WgsDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

        }
    }
}
