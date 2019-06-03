namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStanice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stanicas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Adresa = c.String(),
                        Koordinata_X = c.Double(nullable: false),
                        Koordinata_Y = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Linijas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TipLinije = c.Int(nullable: false),
                        Ime = c.String(),
                        RedniBroj = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Termins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Dan = c.Int(nullable: false),
                        Polazak = c.DateTime(nullable: false),
                        Linija_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Linijas", t => t.Linija_ID)
                .Index(t => t.Linija_ID);
            
            CreateTable(
                "dbo.LinijaStanicas",
                c => new
                    {
                        Linija_ID = c.Int(nullable: false),
                        Stanica_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Linija_ID, t.Stanica_ID })
                .ForeignKey("dbo.Linijas", t => t.Linija_ID, cascadeDelete: true)
                .ForeignKey("dbo.Stanicas", t => t.Stanica_ID, cascadeDelete: true)
                .Index(t => t.Linija_ID)
                .Index(t => t.Stanica_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Termins", "Linija_ID", "dbo.Linijas");
            DropForeignKey("dbo.LinijaStanicas", "Stanica_ID", "dbo.Stanicas");
            DropForeignKey("dbo.LinijaStanicas", "Linija_ID", "dbo.Linijas");
            DropIndex("dbo.LinijaStanicas", new[] { "Stanica_ID" });
            DropIndex("dbo.LinijaStanicas", new[] { "Linija_ID" });
            DropIndex("dbo.Termins", new[] { "Linija_ID" });
            DropTable("dbo.LinijaStanicas");
            DropTable("dbo.Termins");
            DropTable("dbo.Linijas");
            DropTable("dbo.Stanicas");
        }
    }
}
