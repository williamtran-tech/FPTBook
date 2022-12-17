namespace FPTBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notAllowNullForImgPath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "ImagePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "ImagePath", c => c.String());
        }
    }
}
