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
        private static ApplicationSignInManager _signInManager;
        public static ApplicationSignInManager ApplicationSignManager
        {
            get { return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }


        private static ApplicationUserManager _userManager;
        public static ApplicationUserManager ApplicationUserManager
        {
            get { return _userManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        private static WgsDbContext _context;

        public static WgsDbContext Context
        {
            get { return _context ?? HttpContext.Current.GetOwinContext().Get<WgsDbContext>(); }
            private set { _context = value; }
        }
    }
}