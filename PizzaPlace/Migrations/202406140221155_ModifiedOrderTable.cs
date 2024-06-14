namespace PizzaPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("Transaction.Orders", "FileOrderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Transaction.Orders", "FileOrderId");
        }
    }
}
