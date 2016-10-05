using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WGS.Models;
using WGS.Security;

namespace WGS
{
    public class Helpers
    {
        public static ApplicationSignInManager ApplicationSignManager => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
        public static ApplicationUserManager ApplicationUserManager => HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();
        public static WgsDbContext Context => HttpContext.Current.GetOwinContext().Get<WgsDbContext>();

        public static ApplicationRoleManager roleManagerApplicationRoleManager => HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

    }
}