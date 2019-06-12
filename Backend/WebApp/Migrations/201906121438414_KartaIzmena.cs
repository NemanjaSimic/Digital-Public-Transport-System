namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KartaIzmena : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kartas", "Korisnik", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kartas", "Korisnik");
        }
    }
}
