using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WGS.Models;
using WGS.Security;
using WGS.ViewModels;

namespace WGS.Controllers
{
    public class AuthController : Controller
    {


        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager ApplicationSignManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            User user = new User()
            {
                FirstName = "Wajeeh",
                LastName = "Ahmed",
                Email = "wajeehnaeem@yahoo.com",
                UserName = "ahmed"
            };
            HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Create(user, "Wajeeh_ahmed93");


            SignInStatus status = ApplicationSignManager.PasswordSignIn(userName: model.UserName,
                password: model.Password, isPersistent: false, shouldLockout: false);
            switch (status)
            {
                case (SignInStatus.Success):
                    return RedirectToAction(controllerName: "Home", actionName: "About");
                  
                default :
                    return RedirectToAction("Login");
            }

        }
    }
}