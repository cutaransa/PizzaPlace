
using PizzaPlace.Core.Models;
using PizzaPlace.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using Hangfire;
using PizzaPlace.Web.App_Start;
[assembly: OwinStartup(typeof(PizzaPlace.Web.Startup))]

namespace PizzaPlace.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            InitialDatabaseSeed();

            // Configure Hangfire for background job services
            GlobalConfiguration.Configuration.UseSqlServerStorage("DatabaseConnection");
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire-jobs",
                new DashboardOptions { AuthorizationFilters = new List<HangFireAuthorizationFilter>() });

            ConfigureAuth(app);

        }

        private void InitialDatabaseSeed()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Creating first Administrator Role and creating a default Administrator User    
            if (!roleManager.RoleExists("Administrator"))
            {
                // first we create Admin role   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Id = "c7c06140-a28f-43d7-94ac-d0f6e3dd8c01";
                role.Name = "Administrator";
                roleManager.Create(role);

                //Create a Administrator super user who will maintain the website
                var user = new ApplicationUser();
                user.Id = "bf510c7e-617a-4407-8396-5d49d7701c5d";
                user.UserName = "cutaransa@gmail.com";
                user.Email = "cutaransa@gmail.com";
                string userPWD = "CutaranSJP@" + DateTime.Now.Year.ToString();

                var chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Administrator");
                    var admin = new Administrator()
                    {
                        AdministratorId = user.Id,
                        Name = "Sir John Paul Cutaran",
                        IsDefault = true,
                        IsActive = true
                    };
                    context.Administrators.Add(admin);
                    context.SaveChanges();
                }
            }
        }
    }
}