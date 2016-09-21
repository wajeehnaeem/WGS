using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WGS.Models;

namespace WGS.Security
{
    public class ApplicationSignInManager : SignInManager<User, String>
    {
        public ApplicationSignInManager(UserManager<User, string> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {

        }

        public static ApplicationSignInManager Create()
        {
            return new ApplicationSignInManager(HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(),HttpContext.Current.GetOwinContext().Authentication);
        }
    }
}