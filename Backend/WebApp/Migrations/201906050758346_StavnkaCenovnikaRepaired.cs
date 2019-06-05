namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StavnkaCenovnikaRepaired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", "dbo.TipKartes");
            DropForeignKey("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", "dbo.TipPopustas");
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipKarte_VrstaKarte" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipPopusta_VrstaPopusta" });
            DropColumn("dbo.StavkaCenovnikas", "TipKarteId");
            DropColumn("dbo.StavkaCenovnikas", "TipPopustaId");
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "TipKarte_VrstaKarte", newName: "TipKarteId");
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "TipPopusta_VrstaPopusta", newName: "TipPopustaId");
            AddColumn("dbo.StavkaCenovnikas", "Cena", c => c.Single(nullable: false));
            AlterColumn("dbo.StavkaCenovnikas", "TipKarteId", c => c.Int(nullable: false));
            AlterColumn("dbo.StavkaCenovnikas", "TipPopustaId", c => c.Int(nullable: false));
            CreateIndex("dbo.StavkaCenovnikas", "TipKarteId");
            CreateIndex("dbo.StavkaCenovnikas", "TipPopustaId");
            AddForeignKey("dbo.StavkaCenovnikas", "TipKarteId", "dbo.TipKartes", "VrstaKarte", cascadeDelete: true);
            AddForeignKey("dbo.StavkaCenovnikas", "TipPopustaId", "dbo.TipPopustas", "VrstaPopusta", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StavkaCenovnikas", "TipPopustaId", "dbo.TipPopustas");
            DropForeignKey("dbo.StavkaCenovnikas", "TipKarteId", "dbo.TipKartes");
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipPopustaId" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipKarteId" });
            AlterColumn("dbo.StavkaCenovnikas", "TipPopustaId", c => c.Int());
            AlterColumn("dbo.StavkaCenovnikas", "TipKarteId", c => c.Int());
            DropColumn("dbo.StavkaCenovnikas", "Cena");
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "TipPopustaId", newName: "TipPopusta_VrstaPopusta");
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "TipKarteId", newName: "TipKarte_VrstaKarte");
            AddColumn("dbo.StavkaCenovnikas", "TipPopustaId", c => c.Int(nullable: false));
            AddColumn("dbo.StavkaCenovnikas", "TipKarteId", c => c.Int(nullable: false));
            CreateIndex("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta");
            CreateIndex("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte");
            AddForeignKey("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", "dbo.TipPopustas", "VrstaPopusta");
            AddForeignKey("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", "dbo.TipKartes", "VrstaKarte");
        }
    }
}
