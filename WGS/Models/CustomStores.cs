﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace WGS.Models
{
    public class CustomUserStore : UserStore<AppUser>
    {
        public CustomUserStore() : base(HttpContext.Current.GetOwinContext().Get<WgsDbContext>())
        {

        }
    }
    public class CustomRoleStore : RoleStore<IdentityRole>
    {
        public CustomRoleStore() : base(HttpContext.Current.GetOwinContext().Get<WgsDbContext>())
        {

        }
    }

}