namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptimisticConcurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stanicas", "Verzija", c => c.Int(nullable: false));
			AddColumn("dbo.Liijas", "Verzija", c => c.Int(nullable: false));
		}
        
        public override void Down()
        {
            DropColumn("dbo.Stanicas", "Verzija");
			DropColumn("dbo.Liijas", "Verzija");
		}
    }
}
