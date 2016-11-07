namespace Giffy.DataAccess.Migrations.Identity
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Entities.Models;
    using Microsoft.AspNet.Identity;
    using Entities.Models.Enums;
    using Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<Giffy.DataAccess.GiffyIdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\Identity";
        }

        protected override void Seed(Giffy.DataAccess.GiffyIdentityContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<User>(new UserStore<User>(new GiffyIdentityContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new GiffyIdentityContext()));

            var user = new User()
            {
                UserName = "GiffySA",
                Email = "troyuit@gmail.com",
                EmailConfirmed = true,
                FirstName = "Troy",
                LastName = "Lee",
                Level = 1,
                JoinedDate = DateTime.UtcNow.AddYears(-3)
            };

            manager.Create(user, "Asdcxz1+");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Mod" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByName("GiffySA");

            manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });

            if (context.Clients.Count(c => c.Id == "Web.BongVL") == 0)
            {
                context.Clients.Add(new Client
                {
                    Id = "Web.BongVL",
                    Secret = HashHelper.GetSHA256("Asdcxz1+"),
                    Name = "BongVL Web Application",
                    ApplicationType = ApplicationType.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "http://bongvl.vn"
                });
                context.SaveChanges();
            }
        }
    }
}
