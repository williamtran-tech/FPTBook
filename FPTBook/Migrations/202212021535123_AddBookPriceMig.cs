namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookPriceMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Description", c => c.String(nullable: false));
            AddColumn("dbo.Books", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.Books", "Body");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Body", c => c.String(nullable: false));
            DropColumn("dbo.Books", "Price");
            DropColumn("dbo.Books", "Description");
        }
    }
}
