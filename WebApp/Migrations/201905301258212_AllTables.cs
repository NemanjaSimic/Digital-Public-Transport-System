namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LinijaStanicas", newName: "StanicaLinijas");
            DropForeignKey("dbo.Stanicas", "Koordinata_Id", "dbo.Koordinatas");
            DropIndex("dbo.Stanicas", new[] { "Koordinata_Id" });
            RenameColumn(table: "dbo.Stanicas", name: "Koordinata_Id", newName: "KoordinataId");
            DropPrimaryKey("dbo.StanicaLinijas");
            CreateTable(
                "dbo.Cenovniks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Od = c.DateTime(nullable: false),
                        Do = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StavkaCenovnikas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipKarteId = c.Int(nullable: false),
                        TipPopustaId = c.Int(nullable: false),
                        CenovnikId = c.Int(nullable: false),
                        TipKarte_VrstaKarte = c.Int(),
                        TipPopusta_VrstaPopusta = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipKartes", t => t.TipKarte_VrstaKarte)
                .ForeignKey("dbo.TipPopustas", t => t.TipPopusta_VrstaPopusta)
                .ForeignKey("dbo.Cenovniks", t => t.CenovnikId, cascadeDelete: true)
                .Index(t => t.CenovnikId)
                .Index(t => t.TipKarte_VrstaKarte)
                .Index(t => t.TipPopusta_VrstaPopusta);
            
            CreateTable(
                "dbo.TipKartes",
                c => new
                    {
                        VrstaKarte = c.Int(nullable: false),
                        CenaKarte = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.VrstaKarte);
            
            CreateTable(
                "dbo.Kartas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Validna = c.Boolean(nullable: false),
                        DatumIzdavanja = c.DateTime(nullable: false),
                        StavkaCenovnikaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StavkaCenovnikas", t => t.StavkaCenovnikaId, cascadeDelete: true)
                .Index(t => t.StavkaCenovnikaId);
            
            AlterColumn("dbo.Stanicas", "KoordinataId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.StanicaLinijas", new[] { "Stanica_ID", "Linija_ID" });
            CreateIndex("dbo.Stanicas", "KoordinataId");
            AddForeignKey("dbo.Stanicas", "KoordinataId", "dbo.Koordinatas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stanicas", "KoordinataId", "dbo.Koordinatas");
            DropForeignKey("dbo.Kartas", "StavkaCenovnikaId", "dbo.StavkaCenovnikas");
            DropForeignKey("dbo.StavkaCenovnikas", "CenovnikId", "dbo.Cenovniks");
            DropForeignKey("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", "dbo.TipPopustas");
            DropForeignKey("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", "dbo.TipKartes");
            DropIndex("dbo.Stanicas", new[] { "KoordinataId" });
            DropIndex("dbo.Kartas", new[] { "StavkaCenovnikaId" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipPopusta_VrstaPopusta" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipKarte_VrstaKarte" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "CenovnikId" });
            DropPrimaryKey("dbo.StanicaLinijas");
            AlterColumn("dbo.Stanicas", "KoordinataId", c => c.Int());
            DropTable("dbo.Kartas");
            DropTable("dbo.TipKartes");
            DropTable("dbo.StavkaCenovnikas");
            DropTable("dbo.Cenovniks");
            AddPrimaryKey("dbo.StanicaLinijas", new[] { "Linija_ID", "Stanica_ID" });
            RenameColumn(table: "dbo.Stanicas", name: "KoordinataId", newName: "Koordinata_Id");
            CreateIndex("dbo.Stanicas", "Koordinata_Id");
            AddForeignKey("dbo.Stanicas", "Koordinata_Id", "dbo.Koordinatas", "Id");
            RenameTable(name: "dbo.StanicaLinijas", newName: "LinijaStanicas");
        }
    }
}
