namespace WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
	using System.Collections.Generic;
	using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.

			if (!context.Roles.Any(r => r.Name == "Admin"))
			{
				var store = new RoleStore<IdentityRole>(context);
				var manager = new RoleManager<IdentityRole>(store);
				var role = new IdentityRole { Name = "Admin" };

				manager.Create(role);
			}

			if (!context.Roles.Any(r => r.Name == "Controller"))
			{
				var store = new RoleStore<IdentityRole>(context);
				var manager = new RoleManager<IdentityRole>(store);
				var role = new IdentityRole { Name = "Controller" };

				manager.Create(role);
			}

			if (!context.Roles.Any(r => r.Name == "AppUser"))
			{
				var store = new RoleStore<IdentityRole>(context);
				var manager = new RoleManager<IdentityRole>(store);
				var role = new IdentityRole { Name = "AppUser" };

				manager.Create(role);
			}

			var userStore = new UserStore<ApplicationUser>(context);
			var userManager = new UserManager<ApplicationUser>(userStore);

			if (!context.Users.Any(u => u.UserName == "admin@yahoo.com"))
			{
				var user = new ApplicationUser() { Id = "admin", UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!") };
				userManager.Create(user);
				userManager.AddToRole(user.Id, "Admin");
			}

			if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
			{
				var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo.com", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!") };
				userManager.Create(user);
				userManager.AddToRole(user.Id, "AppUser");
			}
			//Termin termin1 = new Termin() { Dan = Models.Enums.Dan.Subota, Polazak = new TimeSpan(10,0,0) };
			//Termin termin2 = new Termin() { Dan = Models.Enums.Dan.Nedelja, Polazak = new TimeSpan(10, 0, 0) };
			//Termin termin3 = new Termin() { Dan = Models.Enums.Dan.Subota, Polazak = new TimeSpan(11, 0, 0) };
			//Termin termin4 = new Termin() { Dan = Models.Enums.Dan.Subota, Polazak = new TimeSpan(12, 0, 0) };
			//Termin termin5 = new Termin() { Dan = Models.Enums.Dan.RadniDan, Polazak = new TimeSpan(10, 0, 0) };
			//Termin termin6 = new Termin() { Dan = Models.Enums.Dan.RadniDan, Polazak = new TimeSpan(11, 0, 0) };
			//Termin termin7 = new Termin() { Dan = Models.Enums.Dan.RadniDan, Polazak = new TimeSpan(12, 0, 0) };
			//Termin termin8 = new Termin() { Dan = Models.Enums.Dan.RadniDan, Polazak = new TimeSpan(13, 0, 0) };
			//Termin termin9 = new Termin() { Dan = Models.Enums.Dan.RadniDan, Polazak = new TimeSpan(14, 0, 0) };
			//Termin termin10 = new Termin() { Dan = Models.Enums.Dan.RadniDan, Polazak = new TimeSpan(15, 0, 0) };
			//Koordinata kor = new Koordinata() { X = 45.24, Y = 43.65 };
			//Stanica stan = new Stanica() { Naziv = "Sajam", Adresa = "Bulevar Oslobodjenja 143", Koordinata = kor };
			//Linija linija = new Linija() { Ime = "Liman-Novo naselje", RedniBroj = 7, Stanice = new List<Stanica>() { stan },
			//	Termini = new List<Termin>() { termin1, termin2, termin3, termin4, termin5, termin6, termin7, termin8, termin9, termin10 },
			//	TipLinije = Models.Enums.VrstaLinije.Gradski };
			//Linija linija2 = new Linija()
			//{
			//	Ime = "Petrovaradin-Zeleznicka",
			//	RedniBroj = 11,
			//	Stanice = new List<Stanica>() { stan },
			//	Termini = new List<Termin>() { termin1, termin2, termin3, termin4, termin5, termin6, termin7, termin8, termin9, termin10 },
			//	TipLinije = Models.Enums.VrstaLinije.Prigradski
			//};
			//Linija linija3 = new Linija()
			//{
			//	Ime = "Liman-Sajam",
			//	RedniBroj = 4,
			//	Stanice = new List<Stanica>() { stan },
			//	Termini = new List<Termin>() { termin1, termin2, termin3, termin4, termin8, termin9, termin10 },
			//	TipLinije = Models.Enums.VrstaLinije.Gradski
			//};
			//stan.Linije = new List<Linija>() { linija };
			//termin1.Linije = new List<Linija>() { linija, linija2, linija3 };
			//termin2.Linije = new List<Linija>() { linija, linija2, linija3 };
			//termin3.Linije = new List<Linija>() { linija, linija2, linija3 };
			//termin4.Linije = new List<Linija>() { linija, linija2, linija3 };
			//termin5.Linije = new List<Linija>() { linija, linija2 };
			//termin6.Linije = new List<Linija>() { linija, linija2 };
			//termin7.Linije = new List<Linija>() { linija, linija2 };
			//termin8.Linije = new List<Linija>() { linija, linija2, linija3 };
			//termin9.Linije = new List<Linija>() { linija, linija2, linija3 };
			//termin10.Linije = new List<Linija>() { linija, linija2, linija3 };

			//context.Koordinate.Add(kor);
			//context.Stanice.Add(stan);
			//context.Linije.Add(linija);
			//context.Linije.Add(linija2);
			//context.Linije.Add(linija3);

			//context.Termini.Add(termin1);
			//context.Termini.Add(termin2);
			//context.Termini.Add(termin3);
			//context.Termini.Add(termin4);
			//context.Termini.Add(termin5);
			//context.Termini.Add(termin6);
			//context.Termini.Add(termin7);
			//context.Termini.Add(termin8);
			//context.Termini.Add(termin9);
			//context.Termini.Add(termin10);
			//context.Stanice.Add(new Stanica() { Naziv = "Sajam", Adresa = "Bulevar Oslobodjenja 143" , Koordinata = new Koordinata() { } });
			//context.TipPopustas.Add(new TipPopusta() { VrstaPopusta = Models.Enums.VrstaPopusta.Djacka, Koeficijent = (float)0.6 });
			//cen.Aktuelan = true;
			//var tipKarte = new TipKarte() { VrstaKarte = Models.Enums.VrstaKarte.Vremenska, CenaKarte = 120 };
			//var tipPopusta = new TipPopusta() {VrstaPopusta = Models.Enums.VrstaPopusta.Penzionerska, Koeficijent = (float)0.4 };
			//var cen = new Cenovnik() { Od = new DateTime(2019, 1, 1), Do = new DateTime(2019, 12, 31) };
			//context.Cenovnici.Add(cen);
			//context.TipKartes.Add(tipKarte);
			//context.TipPopustas.Add(tipPopusta);
			//var tipKarte = context.TipKartes.ToList().Find(k => k.VrstaKarte == Models.Enums.VrstaKarte.Vremenska);
			//var tipPopusta = context.TipPopustas.ToList().Find(p => p.VrstaPopusta == Models.Enums.VrstaPopusta.Regularna);
			//var cen = context.Cenovnici.ToList().FirstOrDefault();
			//context.Stavke.Add(new StavkaCenovnika() { Cena = 120, TipKarte = tipKarte, TipPopusta = tipPopusta, Cenovnik = cen });

			//context.SaveChanges();
		}
    }
}
