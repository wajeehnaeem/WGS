using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WGS.Models;

namespace WGS.Security
{
    public class ApplicationUserManager : UserManager<User, String>
    {
        public ApplicationUserManager() : base(new CustomUserStore())
        {

        }

        public static ApplicationUserManager Create()
        {
            return new ApplicationUserManager();
        }
    }
}