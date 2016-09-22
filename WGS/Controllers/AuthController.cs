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


       

        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            SignInStatus status = Helpers.ApplicationSignManager.PasswordSignIn(userName: model.UserName,
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