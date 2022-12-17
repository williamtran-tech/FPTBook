namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImgPathForBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "ImagePath");
        }
    }
}
