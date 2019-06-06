namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repaired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StavkaCenovnikas", "CenovnikId", "dbo.Cenovniks");
            DropForeignKey("dbo.StavkaCenovnikas", "TipKarteId", "dbo.TipKartes");
            DropForeignKey("dbo.StavkaCenovnikas", "TipPopustaId", "dbo.TipPopustas");
            DropForeignKey("dbo.Kartas", "StavkaCenovnikaId", "dbo.StavkaCenovnikas");
            DropForeignKey("dbo.Stanicas", "KoordinataId", "dbo.Koordinatas");
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipKarteId" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipPopustaId" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "CenovnikId" });
            DropIndex("dbo.Kartas", new[] { "StavkaCenovnikaId" });
            DropIndex("dbo.Stanicas", new[] { "KoordinataId" });
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "CenovnikId", newName: "Cenovnik_ID");
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "TipKarteId", newName: "TipKarte_VrstaKarte");
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "TipPopustaId", newName: "TipPopusta_VrstaPopusta");
            RenameColumn(table: "dbo.Kartas", name: "StavkaCenovnikaId", newName: "StavkaCenovnika_Id");
            RenameColumn(table: "dbo.Stanicas", name: "KoordinataId", newName: "Koordinata_Id");
            AlterColumn("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", c => c.Int());
            AlterColumn("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", c => c.Int());
            AlterColumn("dbo.StavkaCenovnikas", "Cenovnik_ID", c => c.Int());
            AlterColumn("dbo.Kartas", "StavkaCenovnika_Id", c => c.Int());
            AlterColumn("dbo.Stanicas", "Koordinata_Id", c => c.Int());
            CreateIndex("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte");
            CreateIndex("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta");
            CreateIndex("dbo.StavkaCenovnikas", "Cenovnik_ID");
            CreateIndex("dbo.Kartas", "StavkaCenovnika_Id");
            CreateIndex("dbo.Stanicas", "Koordinata_Id");
            AddForeignKey("dbo.StavkaCenovnikas", "Cenovnik_ID", "dbo.Cenovniks", "ID");
            AddForeignKey("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", "dbo.TipKartes", "VrstaKarte");
            AddForeignKey("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", "dbo.TipPopustas", "VrstaPopusta");
            AddForeignKey("dbo.Kartas", "StavkaCenovnika_Id", "dbo.StavkaCenovnikas", "Id");
            AddForeignKey("dbo.Stanicas", "Koordinata_Id", "dbo.Koordinatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stanicas", "Koordinata_Id", "dbo.Koordinatas");
            DropForeignKey("dbo.Kartas", "StavkaCenovnika_Id", "dbo.StavkaCenovnikas");
            DropForeignKey("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", "dbo.TipPopustas");
            DropForeignKey("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", "dbo.TipKartes");
            DropForeignKey("dbo.StavkaCenovnikas", "Cenovnik_ID", "dbo.Cenovniks");
            DropIndex("dbo.Stanicas", new[] { "Koordinata_Id" });
            DropIndex("dbo.Kartas", new[] { "StavkaCenovnika_Id" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "Cenovnik_ID" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipPopusta_VrstaPopusta" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipKarte_VrstaKarte" });
            AlterColumn("dbo.Stanicas", "Koordinata_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Kartas", "StavkaCenovnika_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.StavkaCenovnikas", "Cenovnik_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", c => c.Int(nullable: false));
            AlterColumn("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Stanicas", name: "Koordinata_Id", newName: "KoordinataId");
            RenameColumn(table: "dbo.Kartas", name: "StavkaCenovnika_Id", newName: "StavkaCenovnikaId");
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "TipPopusta_VrstaPopusta", newName: "TipPopustaId");
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "TipKarte_VrstaKarte", newName: "TipKarteId");
            RenameColumn(table: "dbo.StavkaCenovnikas", name: "Cenovnik_ID", newName: "CenovnikId");
            CreateIndex("dbo.Stanicas", "KoordinataId");
            CreateIndex("dbo.Kartas", "StavkaCenovnikaId");
            CreateIndex("dbo.StavkaCenovnikas", "CenovnikId");
            CreateIndex("dbo.StavkaCenovnikas", "TipPopustaId");
            CreateIndex("dbo.StavkaCenovnikas", "TipKarteId");
            AddForeignKey("dbo.Stanicas", "KoordinataId", "dbo.Koordinatas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Kartas", "StavkaCenovnikaId", "dbo.StavkaCenovnikas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StavkaCenovnikas", "TipPopustaId", "dbo.TipPopustas", "VrstaPopusta", cascadeDelete: true);
            AddForeignKey("dbo.StavkaCenovnikas", "TipKarteId", "dbo.TipKartes", "VrstaKarte", cascadeDelete: true);
            AddForeignKey("dbo.StavkaCenovnikas", "CenovnikId", "dbo.Cenovniks", "ID", cascadeDelete: true);
        }
    }
}
