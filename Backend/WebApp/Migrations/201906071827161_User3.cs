namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: true));
            AlterColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: true));
            AlterColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: true));
            AlterColumn("dbo.AspNetUsers", "ImgUrl", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "ImgUrl", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Address", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
    }
}
