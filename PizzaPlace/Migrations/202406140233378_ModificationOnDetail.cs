namespace PizzaPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificationOnDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("Transaction.OrderDetails", "FileDetailId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Transaction.OrderDetails", "FileDetailId");
        }
    }
}
