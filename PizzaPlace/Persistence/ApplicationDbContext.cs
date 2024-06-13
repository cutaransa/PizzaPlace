using Castle.Core.Resource;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.BuilderProperties;
using Ninject.Activation;
using Ninject.Planning.Bindings;
using PizzaPlace.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<RoleModule> RoleModules { get; set; }

        public ApplicationDbContext()
            : base("DatabaseConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                 .ToTable(tableName: "AspNetUsers", schemaName: "User")
                 .Property(e => e.Id).HasColumnName("AspNetUserId");

            modelBuilder.Entity<IdentityRole>()
                .ToTable(tableName: "AspNetRoles", schemaName: "User")
                .Property(e => e.Id).HasColumnName("AspNetRoleId");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("AspNetUserRoles", "User")
                .Property(e => e.UserId)
                .HasColumnName("AspNetUserId");
            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("AspNetUserRoles", "User")
                .Property(e => e.RoleId)
                .HasColumnName("AspNetRoleId");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable(tableName: "AspNetUserClaims", schemaName: "User")
                .Property(e => e.Id)
                .HasColumnName("AspNetRoleClaimId");
            modelBuilder.Entity<IdentityUserClaim>()
                 .ToTable(tableName: "AspNetUserClaims", schemaName: "User")
                 .Property(e => e.UserId)
                 .HasColumnName("AspNetUserId");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable(tableName: "AspNetUserLogins", schemaName: "User")
                .Property(e => e.LoginProvider)
                .HasColumnName("LoginProvider");
            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable(tableName: "AspNetUserLogins", schemaName: "User")
                .Property(e => e.ProviderKey)
                .HasColumnName("ProviderKey");
            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable(tableName: "AspNetUserLogins", schemaName: "User")
                .Property(e => e.UserId)
                .HasColumnName("UserId");
        }
    }
}
