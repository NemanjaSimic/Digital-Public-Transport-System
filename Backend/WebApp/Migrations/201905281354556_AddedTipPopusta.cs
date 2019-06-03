namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTipPopusta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipPopustas",
                c => new
                    {
                        VrstaPopusta = c.Int(nullable: false),
                        Koeficijent = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.VrstaPopusta);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TipPopustas");
        }
    }
}
