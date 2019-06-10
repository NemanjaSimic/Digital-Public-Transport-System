namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cenovniks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Od = c.DateTime(nullable: false),
                        Do = c.DateTime(nullable: false),
                        Aktuelan = c.Boolean(nullable: false),
                        Izbrisano = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StavkaCenovnikas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cena = c.Single(nullable: false),
                        TipKarte_VrstaKarte = c.Int(),
                        TipPopusta_VrstaPopusta = c.Int(),
                        Cenovnik_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipKartes", t => t.TipKarte_VrstaKarte)
                .ForeignKey("dbo.TipPopustas", t => t.TipPopusta_VrstaPopusta)
                .ForeignKey("dbo.Cenovniks", t => t.Cenovnik_ID)
                .Index(t => t.TipKarte_VrstaKarte)
                .Index(t => t.TipPopusta_VrstaPopusta)
                .Index(t => t.Cenovnik_ID);
            
            CreateTable(
                "dbo.TipKartes",
                c => new
                    {
                        VrstaKarte = c.Int(nullable: false),
                        CenaKarte = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.VrstaKarte);
            
            CreateTable(
                "dbo.TipPopustas",
                c => new
                    {
                        VrstaPopusta = c.Int(nullable: false),
                        Koeficijent = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.VrstaPopusta);
            
            CreateTable(
                "dbo.Kartas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Validna = c.Boolean(nullable: false),
                        DatumIzdavanja = c.DateTime(nullable: false),
                        Izbrisano = c.Boolean(nullable: false),
                        StavkaCenovnika_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StavkaCenovnikas", t => t.StavkaCenovnika_Id)
                .Index(t => t.StavkaCenovnika_Id);
            
            CreateTable(
                "dbo.Linijas",
                c => new
                    {
                        Ime = c.String(nullable: false, maxLength: 128),
                        TipLinije = c.Int(nullable: false),
                        RedniBroj = c.Int(nullable: false),
                        Izbrisano = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Ime);
            
            CreateTable(
                "dbo.Stanicas",
                c => new
                    {
                        Naziv = c.String(nullable: false, maxLength: 128),
                        Adresa = c.String(),
                        Izbrisano = c.Boolean(nullable: false),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Naziv);
            
            CreateTable(
                "dbo.Termins",
                c => new
                    {
                        Dan = c.Int(nullable: false),
                        Polazak = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => new { t.Dan, t.Polazak });
            
            CreateTable(
                "dbo.StanicaLinijas",
                c => new
                    {
                        Stanica_Naziv = c.String(nullable: false, maxLength: 128),
                        Linija_Ime = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Stanica_Naziv, t.Linija_Ime })
                .ForeignKey("dbo.Stanicas", t => t.Stanica_Naziv, cascadeDelete: true)
                .ForeignKey("dbo.Linijas", t => t.Linija_Ime, cascadeDelete: true)
                .Index(t => t.Stanica_Naziv)
                .Index(t => t.Linija_Ime);
            
            CreateTable(
                "dbo.TerminLinijas",
                c => new
                    {
                        Termin_Dan = c.Int(nullable: false),
                        Termin_Polazak = c.Time(nullable: false, precision: 7),
                        Linija_Ime = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Termin_Dan, t.Termin_Polazak, t.Linija_Ime })
                .ForeignKey("dbo.Termins", t => new { t.Termin_Dan, t.Termin_Polazak }, cascadeDelete: true)
                .ForeignKey("dbo.Linijas", t => t.Linija_Ime, cascadeDelete: true)
                .Index(t => new { t.Termin_Dan, t.Termin_Polazak })
                .Index(t => t.Linija_Ime);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserType", c => c.String());
            AddColumn("dbo.AspNetUsers", "ImgUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "IsVerified", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Izbrisano", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TerminLinijas", "Linija_Ime", "dbo.Linijas");
            DropForeignKey("dbo.TerminLinijas", new[] { "Termin_Dan", "Termin_Polazak" }, "dbo.Termins");
            DropForeignKey("dbo.StanicaLinijas", "Linija_Ime", "dbo.Linijas");
            DropForeignKey("dbo.StanicaLinijas", "Stanica_Naziv", "dbo.Stanicas");
            DropForeignKey("dbo.Kartas", "StavkaCenovnika_Id", "dbo.StavkaCenovnikas");
            DropForeignKey("dbo.StavkaCenovnikas", "Cenovnik_ID", "dbo.Cenovniks");
            DropForeignKey("dbo.StavkaCenovnikas", "TipPopusta_VrstaPopusta", "dbo.TipPopustas");
            DropForeignKey("dbo.StavkaCenovnikas", "TipKarte_VrstaKarte", "dbo.TipKartes");
            DropIndex("dbo.TerminLinijas", new[] { "Linija_Ime" });
            DropIndex("dbo.TerminLinijas", new[] { "Termin_Dan", "Termin_Polazak" });
            DropIndex("dbo.StanicaLinijas", new[] { "Linija_Ime" });
            DropIndex("dbo.StanicaLinijas", new[] { "Stanica_Naziv" });
            DropIndex("dbo.Kartas", new[] { "StavkaCenovnika_Id" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "Cenovnik_ID" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipPopusta_VrstaPopusta" });
            DropIndex("dbo.StavkaCenovnikas", new[] { "TipKarte_VrstaKarte" });
            DropColumn("dbo.AspNetUsers", "Izbrisano");
            DropColumn("dbo.AspNetUsers", "IsVerified");
            DropColumn("dbo.AspNetUsers", "ImgUrl");
            DropColumn("dbo.AspNetUsers", "UserType");
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.TerminLinijas");
            DropTable("dbo.StanicaLinijas");
            DropTable("dbo.Termins");
            DropTable("dbo.Stanicas");
            DropTable("dbo.Linijas");
            DropTable("dbo.Kartas");
            DropTable("dbo.TipPopustas");
            DropTable("dbo.TipKartes");
            DropTable("dbo.StavkaCenovnikas");
            DropTable("dbo.Cenovniks");
        }
    }
}
