namespace PizzaPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedItemAndTransactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Item.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(maxLength: 128),
                        ModifiedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("User.Administrators", t => t.CreatedById)
                .ForeignKey("User.Administrators", t => t.ModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "Transaction.OrderDetails",
                c => new
                    {
                        DetailId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        PizzaId = c.Int(),
                        OrderId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(maxLength: 128),
                        ModifiedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DetailId)
                .ForeignKey("User.Administrators", t => t.CreatedById)
                .ForeignKey("User.Administrators", t => t.ModifiedById)
                .ForeignKey("Transaction.Orders", t => t.OrderId)
                .ForeignKey("Item.Pizzas", t => t.PizzaId)
                .Index(t => t.PizzaId)
                .Index(t => t.OrderId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "Transaction.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(maxLength: 128),
                        ModifiedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("User.Administrators", t => t.CreatedById)
                .ForeignKey("User.Administrators", t => t.ModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "Item.Pizzas",
                c => new
                    {
                        PizzaId = c.Int(nullable: false, identity: true),
                        TypeId = c.Int(),
                        Size = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(maxLength: 128),
                        ModifiedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PizzaId)
                .ForeignKey("User.Administrators", t => t.CreatedById)
                .ForeignKey("User.Administrators", t => t.ModifiedById)
                .ForeignKey("Item.PizzaTypes", t => t.TypeId)
                .Index(t => t.TypeId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
            CreateTable(
                "Item.PizzaTypes",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Ingredients = c.String(),
                        CategoryId = c.Int(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ModifiedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.String(maxLength: 128),
                        ModifiedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TypeId)
                .ForeignKey("Item.Categories", t => t.CategoryId)
                .ForeignKey("User.Administrators", t => t.CreatedById)
                .ForeignKey("User.Administrators", t => t.ModifiedById)
                .Index(t => t.CategoryId)
                .Index(t => t.CreatedById)
                .Index(t => t.ModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Transaction.OrderDetails", "PizzaId", "Item.Pizzas");
            DropForeignKey("Item.Pizzas", "TypeId", "Item.PizzaTypes");
            DropForeignKey("Item.PizzaTypes", "ModifiedById", "User.Administrators");
            DropForeignKey("Item.PizzaTypes", "CreatedById", "User.Administrators");
            DropForeignKey("Item.PizzaTypes", "CategoryId", "Item.Categories");
            DropForeignKey("Item.Pizzas", "ModifiedById", "User.Administrators");
            DropForeignKey("Item.Pizzas", "CreatedById", "User.Administrators");
            DropForeignKey("Transaction.OrderDetails", "OrderId", "Transaction.Orders");
            DropForeignKey("Transaction.Orders", "ModifiedById", "User.Administrators");
            DropForeignKey("Transaction.Orders", "CreatedById", "User.Administrators");
            DropForeignKey("Transaction.OrderDetails", "ModifiedById", "User.Administrators");
            DropForeignKey("Transaction.OrderDetails", "CreatedById", "User.Administrators");
            DropForeignKey("Item.Categories", "ModifiedById", "User.Administrators");
            DropForeignKey("Item.Categories", "CreatedById", "User.Administrators");
            DropIndex("Item.PizzaTypes", new[] { "ModifiedById" });
            DropIndex("Item.PizzaTypes", new[] { "CreatedById" });
            DropIndex("Item.PizzaTypes", new[] { "CategoryId" });
            DropIndex("Item.Pizzas", new[] { "ModifiedById" });
            DropIndex("Item.Pizzas", new[] { "CreatedById" });
            DropIndex("Item.Pizzas", new[] { "TypeId" });
            DropIndex("Transaction.Orders", new[] { "ModifiedById" });
            DropIndex("Transaction.Orders", new[] { "CreatedById" });
            DropIndex("Transaction.OrderDetails", new[] { "ModifiedById" });
            DropIndex("Transaction.OrderDetails", new[] { "CreatedById" });
            DropIndex("Transaction.OrderDetails", new[] { "OrderId" });
            DropIndex("Transaction.OrderDetails", new[] { "PizzaId" });
            DropIndex("Item.Categories", new[] { "ModifiedById" });
            DropIndex("Item.Categories", new[] { "CreatedById" });
            DropTable("Item.PizzaTypes");
            DropTable("Item.Pizzas");
            DropTable("Transaction.Orders");
            DropTable("Transaction.OrderDetails");
            DropTable("Item.Categories");
        }
    }
}
