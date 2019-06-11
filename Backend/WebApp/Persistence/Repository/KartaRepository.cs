using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class KartaRepository : Repository<Karta, int>, IKartaRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public KartaRepository(DbContext context) : base(context)
        {
        }

		public int NapraviKartu(VrstaKarte vrstaKarte, VrstaPopusta vrstaPopusta)
		{
			try
			{
				StavkaCenovnika stavka = new StavkaCenovnika();
				var lista = AppDbContext.Cenovnici.ToList().FirstOrDefault(c => c.Aktuelan == true && !c.Izbrisano).Stavke;
				stavka = lista.Find(s => s.TipKarte.VrstaKarte == vrstaKarte && s.TipPopusta.VrstaPopusta == vrstaPopusta);
				var karta = new Karta() { DatumIzdavanja = DateTime.Now, Validna = true, StavkaCenovnika = stavka };
				AppDbContext.Karte.Add(karta);
				AppDbContext.SaveChanges();
				return karta.ID;
			}
			catch (Exception)
			{
				return -1;
			}

		}
	}
}