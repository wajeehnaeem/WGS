using System.Collections.Generic;
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
            List<Level> Levels = new List<Level>
            {
                new Level {Name="Class 1"},
                new Level {Name="Class 2" },

                new Level {Name="Class 3"},
                new Level {Name="Class 4" },
                new Level {Name="Class 5"},
                new Level {Name="Class 6" },
                new Level {Name="Class 7"},
                new Level {Name="Class 8" },
                new Level {Name="Class 9"},
                new Level {Name="Class 10" },
                new Level {Name="Entrance Test"},

                new Level {Name="Entrance Test Class 1"},
                new Level {Name="Entrance Test Class 2" },

                new Level {Name="Entrance Test Class 3"},
                new Level {Name="Entrance Test Class 4" },
                new Level {Name="Entrance Test Class 5"},
                new Level {Name="Entrance Test Class 6" },
                new Level {Name="Entrance Test Class 7"},
                new Level {Name="Entrance Test Class 8" },
                new Level {Name="Entrance Test Class 9"},
                new Level {Name="Entrance Test Class 10" },

            };
            Levels.ForEach(l =>
            {
                if (!context.Levels.Any(level => level.Name == l.Name))
                {
                    context.Levels.Add(l);
                }

            });
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            AppUser user = new AppUser()
            {
                FirstName = "Wajeeh Ahmed",
                LastName = "Siddiqui",
                Email = "wajeehnaeem@yahoo.com",
                UserName = "wajeehnaeem",
                Password = "Wajeeh_ahmed93"

            };

            userManager.Create(user, "Wajeeh_ahmed93");

            var adminUser = userManager.FindByEmail("wajeehnaeem@yahoo.com");
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roleManager.Create(new IdentityRole("Administrator"));
            roleManager.Create(new IdentityRole("Student"));
            roleManager.Create(new IdentityRole("Instructor"));
            roleManager.Create(new IdentityRole("Examiner"));
            roleManager.Create(new IdentityRole("Exam Preparer"));

           userManager.AddToRoles(adminUser.Id, "Administrator", "Student", "Instructor", "Examiner", "Exam Preparer");


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
