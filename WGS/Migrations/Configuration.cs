using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WGS.Models;
using WGS.Security;

namespace WGS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WGS.Models.WgsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WGS.Models.WgsDbContext context)
        {
            //User user = new User()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Email = "wajeehnaeem@yahoo.com",
            //    PasswordHash = new PasswordHasher().HashPassword("Wajeeh_ahmed93"),
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //    TwoFactorEnabled = false,
            //    LockoutEnabled = false,
            //    AccessFailedCount = 0,
            //    UserName = "ahmed",
            //    FirstName = "Wajeeh",
            //    LastName = "Ahmed"
            //};
            //IdentityRole role = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Admin" };
            //context.Roles.Add(role);
            //user.Roles.Add(new IdentityUserRole() { RoleId = role.Id, UserId = user.Id });
            //context.Users.Add(user);
        }
    }
}
