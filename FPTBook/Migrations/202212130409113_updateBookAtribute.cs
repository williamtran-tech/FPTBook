namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateBookAtribute : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Books", new[] { "Author_Id" });
            AddColumn("dbo.Books", "Author", c => c.String());
            DropColumn("dbo.Books", "Author_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Author_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Books", "Author");
            CreateIndex("dbo.Books", "Author_Id");
            AddForeignKey("dbo.Books", "Author_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
