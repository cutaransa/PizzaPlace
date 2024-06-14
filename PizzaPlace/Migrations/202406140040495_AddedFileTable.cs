namespace PizzaPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "User.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FileDestination = c.String(),
                        TempName = c.String(),
                        TempDestination = c.String(),
                        Description = c.String(),
                        Status = c.String(),
                        Remarks = c.String(),
                        UploadedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UploadedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("User.Administrators", t => t.UploadedById)
                .Index(t => t.UploadedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("User.Files", "UploadedById", "User.Administrators");
            DropIndex("User.Files", new[] { "UploadedById" });
            DropTable("User.Files");
        }
    }
}
