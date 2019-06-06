namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repaired1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StavkaCenovnikas", "Cenovnik_ID", "dbo.Cenovniks");
            DropIndex("dbo.StavkaCenovnikas", new[] { "Cenovnik_ID" });
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "Cenovnik_ID", newName: "CenovnikId");
            AddColumn("dbo.StavkaCenovnikas", "TipKarteId", c => c.Int(nullable: false));
            AddColumn("dbo.StavkaCenovnikas", "TipPopustaId", c => c.Int(nullable: false));
            AlterColumn("dbo.StavkaCenovnikas", "CenovnikId", c => c.Int(nullable: false));
            CreateIndex("dbo.StavkaCenovnikas", "CenovnikId");
            AddForeignKey("dbo.StavkaCenovnikas", "CenovnikId", "dbo.Cenovniks", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StavkaCenovnikas", "CenovnikId", "dbo.Cenovniks");
            DropIndex("dbo.StavkaCenovnikas", new[] { "CenovnikId" });
            AlterColumn("dbo.StavkaCenovnikas", "CenovnikId", c => c.Int());
            DropColumn("dbo.StavkaCenovnikas", "TipPopustaId");
            DropColumn("dbo.StavkaCenovnikas", "TipKarteId");
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "CenovnikId", newName: "Cenovnik_ID");
            CreateIndex("dbo.StavkaCenovnikas", "Cenovnik_ID");
            AddForeignKey("dbo.StavkaCenovnikas", "Cenovnik_ID", "dbo.Cenovniks", "ID");
        }
    }
}
