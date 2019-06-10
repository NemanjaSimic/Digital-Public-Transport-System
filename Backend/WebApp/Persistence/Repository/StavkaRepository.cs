using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class StavkaRepository : Repository<StavkaCenovnika, int>, IStavkaRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public StavkaRepository(DbContext context) : base(context)
        {
        }

		public bool NovaStavkaSaSvimPopustima(TipKarte karta, Cenovnik cenovnik)
		{
			bool result = true;
			try
			{
				var popusti = AppDbContext.TipPopustas.ToList();
				//var tempKarta = AppDbContext.TipKartes.ToList().Find(k => k.VrstaKarte == karta.VrstaKarte);
				var tempCenovnik = AppDbContext.Cenovnici.ToList().Find(c => c.Aktuelan);
				foreach (var item in popusti)
				{
					AppDbContext.Stavke.Add(new StavkaCenovnika()
					{
						TipKarte = karta,
						TipPopusta = item,
						Cena = (item.Koeficijent != 1) ? karta.CenaKarte - (karta.CenaKarte * item.Koeficijent) : karta.CenaKarte,
						Cenovnik = cenovnik
					});
					AppDbContext.SaveChanges();
				}
			}
			catch (Exception)
			{
				result = false;
			}

			return result;
		}
    }
}