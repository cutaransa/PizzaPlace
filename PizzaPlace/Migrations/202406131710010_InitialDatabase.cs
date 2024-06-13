namespace PizzaPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "User.Administrators",
                c => new
                    {
                        AdministratorId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        EmailAddress = c.String(),
                        ApiKey = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AdministratorId)
                .ForeignKey("User.AspNetUsers", t => t.AdministratorId)
                .Index(t => t.AdministratorId);
            
            CreateTable(
                "User.AspNetUsers",
                c => new
                    {
                        AspNetUserId = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.AspNetUserId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "User.AspNetUserClaims",
                c => new
                    {
                        AspNetRoleClaimId = c.Int(nullable: false, identity: true),
                        AspNetUserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.AspNetRoleClaimId)
                .ForeignKey("User.AspNetUsers", t => t.AspNetUserId, cascadeDelete: true)
                .Index(t => t.AspNetUserId);
            
            CreateTable(
                "User.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("User.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "User.AspNetUserRoles",
                c => new
                    {
                        AspNetUserId = c.String(nullable: false, maxLength: 128),
                        AspNetRoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.AspNetUserId, t.AspNetRoleId })
                .ForeignKey("User.AspNetUsers", t => t.AspNetUserId, cascadeDelete: true)
                .ForeignKey("User.AspNetRoles", t => t.AspNetRoleId, cascadeDelete: true)
                .Index(t => t.AspNetUserId)
                .Index(t => t.AspNetRoleId);
            
            CreateTable(
                "User.Modules",
                c => new
                    {
                        ModuleId = c.Int(nullable: false, identity: true),
                        Controller = c.String(nullable: false),
                        Action = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleId);
            
            CreateTable(
                "User.RoleModules",
                c => new
                    {
                        RoleModuleId = c.Int(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        ModuleId = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoleModuleId)
                .ForeignKey("User.Modules", t => t.ModuleId, cascadeDelete: true)
                .ForeignKey("User.AspNetRoles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "User.AspNetRoles",
                c => new
                    {
                        AspNetRoleId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.AspNetRoleId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("User.RoleModules", "RoleId", "User.AspNetRoles");
            DropForeignKey("User.AspNetUserRoles", "AspNetRoleId", "User.AspNetRoles");
            DropForeignKey("User.RoleModules", "ModuleId", "User.Modules");
            DropForeignKey("User.Administrators", "AdministratorId", "User.AspNetUsers");
            DropForeignKey("User.AspNetUserRoles", "AspNetUserId", "User.AspNetUsers");
            DropForeignKey("User.AspNetUserLogins", "UserId", "User.AspNetUsers");
            DropForeignKey("User.AspNetUserClaims", "AspNetUserId", "User.AspNetUsers");
            DropIndex("User.AspNetRoles", "RoleNameIndex");
            DropIndex("User.RoleModules", new[] { "ModuleId" });
            DropIndex("User.RoleModules", new[] { "RoleId" });
            DropIndex("User.AspNetUserRoles", new[] { "AspNetRoleId" });
            DropIndex("User.AspNetUserRoles", new[] { "AspNetUserId" });
            DropIndex("User.AspNetUserLogins", new[] { "UserId" });
            DropIndex("User.AspNetUserClaims", new[] { "AspNetUserId" });
            DropIndex("User.AspNetUsers", "UserNameIndex");
            DropIndex("User.Administrators", new[] { "AdministratorId" });
            DropTable("User.AspNetRoles");
            DropTable("User.RoleModules");
            DropTable("User.Modules");
            DropTable("User.AspNetUserRoles");
            DropTable("User.AspNetUserLogins");
            DropTable("User.AspNetUserClaims");
            DropTable("User.AspNetUsers");
            DropTable("User.Administrators");
        }
    }
}
