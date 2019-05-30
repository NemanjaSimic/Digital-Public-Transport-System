namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablesFinally : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Termins", "Linija_ID", "dbo.Linijas");
            DropIndex("dbo.Termins", new[] { "Linija_ID" });
            CreateTable(
                "dbo.TerminLinijas",
                c => new
                    {
                        Termin_ID = c.Int(nullable: false),
                        Linija_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Termin_ID, t.Linija_ID })
                .ForeignKey("dbo.Termins", t => t.Termin_ID, cascadeDelete: true)
                .ForeignKey("dbo.Linijas", t => t.Linija_ID, cascadeDelete: true)
                .Index(t => t.Termin_ID)
                .Index(t => t.Linija_ID);
            
            DropColumn("dbo.Termins", "Linija_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Termins", "Linija_ID", c => c.Int());
            DropForeignKey("dbo.TerminLinijas", "Linija_ID", "dbo.Linijas");
            DropForeignKey("dbo.TerminLinijas", "Termin_ID", "dbo.Termins");
            DropIndex("dbo.TerminLinijas", new[] { "Linija_ID" });
            DropIndex("dbo.TerminLinijas", new[] { "Termin_ID" });
            DropTable("dbo.TerminLinijas");
            CreateIndex("dbo.Termins", "Linija_ID");
            AddForeignKey("dbo.Termins", "Linija_ID", "dbo.Linijas", "ID");
        }
    }
}
