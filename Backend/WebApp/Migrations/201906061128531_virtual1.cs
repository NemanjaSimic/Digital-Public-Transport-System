namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class virtual1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StavkaCenovnikas", "CenovnikId", "dbo.Cenovniks");
            DropIndex("dbo.StavkaCenovnikas", new[] { "CenovnikId" });
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "CenovnikId", newName: "Cenovnik_ID");
            AlterColumn("dbo.StavkaCenovnikas", "Cenovnik_ID", c => c.Int());
            CreateIndex("dbo.StavkaCenovnikas", "Cenovnik_ID");
            AddForeignKey("dbo.StavkaCenovnikas", "Cenovnik_ID", "dbo.Cenovniks", "ID");
            DropColumn("dbo.StavkaCenovnikas", "TipKarteId");
            DropColumn("dbo.StavkaCenovnikas", "TipPopustaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StavkaCenovnikas", "TipPopustaId", c => c.Int(nullable: false));
            AddColumn("dbo.StavkaCenovnikas", "TipKarteId", c => c.Int(nullable: false));
            DropForeignKey("dbo.StavkaCenovnikas", "Cenovnik_ID", "dbo.Cenovniks");
            DropIndex("dbo.StavkaCenovnikas", new[] { "Cenovnik_ID" });
            AlterColumn("dbo.StavkaCenovnikas", "Cenovnik_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "Cenovnik_ID", newName: "CenovnikId");
            CreateIndex("dbo.StavkaCenovnikas", "CenovnikId");
            AddForeignKey("dbo.StavkaCenovnikas", "CenovnikId", "dbo.Cenovniks", "ID", cascadeDelete: true);
        }
    }
}
