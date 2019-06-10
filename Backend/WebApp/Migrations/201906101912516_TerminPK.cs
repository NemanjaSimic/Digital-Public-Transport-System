namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TerminPK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TerminLinijas", new[] { "Termin_Dan", "Termin_Polazak" }, "dbo.Termins");
            DropIndex("dbo.TerminLinijas", new[] { "Termin_Dan", "Termin_Polazak" });
            RenameColumn(table: "dbo.TerminLinijas", name: "Termin_Dan", newName: "Termin_Id");
            DropPrimaryKey("dbo.Termins");
            DropPrimaryKey("dbo.TerminLinijas");
            AddColumn("dbo.Termins", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Termins", "Id");
            AddPrimaryKey("dbo.TerminLinijas", new[] { "Termin_Id", "Linija_Ime" });
            CreateIndex("dbo.TerminLinijas", "Termin_Id");
            AddForeignKey("dbo.TerminLinijas", "Termin_Id", "dbo.Termins", "Id", cascadeDelete: true);
            DropColumn("dbo.TerminLinijas", "Termin_Polazak");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TerminLinijas", "Termin_Polazak", c => c.Time(nullable: false, precision: 7));
            DropForeignKey("dbo.TerminLinijas", "Termin_Id", "dbo.Termins");
            DropIndex("dbo.TerminLinijas", new[] { "Termin_Id" });
            DropPrimaryKey("dbo.TerminLinijas");
            DropPrimaryKey("dbo.Termins");
            DropColumn("dbo.Termins", "Id");
            AddPrimaryKey("dbo.TerminLinijas", new[] { "Termin_Dan", "Termin_Polazak", "Linija_Ime" });
            AddPrimaryKey("dbo.Termins", new[] { "Dan", "Polazak" });
            RenameColumn(table: "dbo.TerminLinijas", name: "Termin_Id", newName: "Termin_Dan");
            CreateIndex("dbo.TerminLinijas", new[] { "Termin_Dan", "Termin_Polazak" });
            AddForeignKey("dbo.TerminLinijas", new[] { "Termin_Dan", "Termin_Polazak" }, "dbo.Termins", new[] { "Dan", "Polazak" }, cascadeDelete: true);
        }
    }
}
