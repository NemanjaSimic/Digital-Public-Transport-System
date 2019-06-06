namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StavkaCenovnikas", "TipKarteId", "dbo.TipKartes");
            DropForeignKey("dbo.StavkaCenovnikas", "TipPopustaId", "dbo.TipPopustas");
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipKarteId" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipPopustaId" });
            AddColumn("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", c => c.Int());
            AddColumn("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", c => c.Int());
            CreateIndex("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte");
            CreateIndex("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta");
            AddForeignKey("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", "dbo.TipKartes", "VrstaKarte");
            AddForeignKey("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", "dbo.TipPopustas", "VrstaPopusta");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", "dbo.TipPopustas");
            DropForeignKey("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", "dbo.TipKartes");
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipPopusta_VrstaPopusta" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipKarte_VrstaKarte" });
            DropColumn("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta");
            DropColumn("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte");
            CreateIndex("dbo.StavkaCenovnikas", "TipPopustaId");
            CreateIndex("dbo.StavkaCenovnikas", "TipKarteId");
            AddForeignKey("dbo.StavkaCenovnikas", "TipPopustaId", "dbo.TipPopustas", "VrstaPopusta", cascadeDelete: true);
            AddForeignKey("dbo.StavkaCenovnikas", "TipKarteId", "dbo.TipKartes", "VrstaKarte", cascadeDelete: true);
        }
    }
}
