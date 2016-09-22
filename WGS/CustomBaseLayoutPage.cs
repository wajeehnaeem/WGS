using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WGS.Models;

namespace WGS
{
    public abstract class AppViewPage<TModel> : WebViewPage<TModel>
    {
        protected User CurrentUser => new User (this.User as ClaimsPrincipal);
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {
    }
   
}