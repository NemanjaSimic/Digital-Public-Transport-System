using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class TipKarteRepository : Repository<TipKarte, VrstaKarte>, ITipKarteRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public TipKarteRepository(DbContext context) : base(context)
        {

        }


		public bool DodajTipKarte(TipKarte tipKarte)
		{
			bool result = true;
			try
			{
				var tempTipKarta = AppDbContext.TipKartes.ToList().FirstOrDefault(t => t.VrstaKarte == tipKarte.VrstaKarte);
				if (tempTipKarta != null)
				{
					tempTipKarta.CenaKarte = tipKarte.CenaKarte;
				}
				else
				{
					AppDbContext.TipKartes.Add(tipKarte);
				}
				AppDbContext.SaveChanges();
			}
			catch (Exception)
			{
				result = false;
				throw;
			}
			return result;
		}
    }
}