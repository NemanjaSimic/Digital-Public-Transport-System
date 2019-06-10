namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userStatusZahteva : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "IsVerified", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "IsVerified", c => c.Boolean(nullable: false));
        }
    }
}
