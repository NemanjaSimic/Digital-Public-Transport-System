namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Koordinatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Stanicas", "Koordinata_Id", c => c.Int());
            CreateIndex("dbo.Stanicas", "Koordinata_Id");
            AddForeignKey("dbo.Stanicas", "Koordinata_Id", "dbo.Koordinatas", "Id");
            DropColumn("dbo.Stanicas", "Koordinata_X");
            DropColumn("dbo.Stanicas", "Koordinata_Y");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stanicas", "Koordinata_Y", c => c.Double(nullable: false));
            AddColumn("dbo.Stanicas", "Koordinata_X", c => c.Double(nullable: false));
            DropForeignKey("dbo.Stanicas", "Koordinata_Id", "dbo.Koordinatas");
            DropIndex("dbo.Stanicas", new[] { "Koordinata_Id" });
            DropColumn("dbo.Stanicas", "Koordinata_Id");
            DropTable("dbo.Koordinatas");
        }
    }
}
