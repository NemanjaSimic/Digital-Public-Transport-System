namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CenovnikUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cenovniks", "Aktuelan", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cenovniks", "Aktuelan");
        }
    }
}
