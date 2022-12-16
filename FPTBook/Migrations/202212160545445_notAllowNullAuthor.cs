namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notAllowNullAuthor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Author", c => c.String());
        }
    }
}
