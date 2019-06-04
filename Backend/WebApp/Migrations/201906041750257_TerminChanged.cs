namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerminChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Termins", "Polazak", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Termins", "Polazak", c => c.DateTime(nullable: false));
        }
    }
}
