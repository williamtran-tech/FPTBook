namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationshipOrderandUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Username_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "Username_Id");
            AddForeignKey("dbo.Orders", "Username_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Orders", "Username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Username", c => c.String());
            DropForeignKey("dbo.Orders", "Username_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "Username_Id" });
            DropColumn("dbo.Orders", "Username_Id");
        }
    }
}
