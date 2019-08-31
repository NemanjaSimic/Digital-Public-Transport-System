namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayPal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kartas", "IdTransakcije", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kartas", "IdTransakcije");
        }
    }
}
