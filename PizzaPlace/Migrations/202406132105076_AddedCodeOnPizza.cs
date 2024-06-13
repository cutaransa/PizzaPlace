namespace PizzaPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCodeOnPizza : DbMigration
    {
        public override void Up()
        {
            AddColumn("Item.Pizzas", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Item.Pizzas", "Code");
        }
    }
}
