using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WGS.Models;

namespace WGS.Security
{
    public class ApplicationRoleManager : RoleManager<IdentityRole> 
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }

        public static ApplicationRoleManager Create()
        {
            return new ApplicationRoleManager(new CustomRoleStore());
        }
    }
}