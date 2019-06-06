using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class StavkaRepository : Repository<StavkaCenovnika, int>, IStavkaRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public StavkaRepository(DbContext context) : base(context)
        {
        }

		public bool DeleteStavka(StavkaBindingModel stavka)
		{
			bool result = true;
			try
			{
				var stavke = AppDbContext.Stavke.ToList();
				stavke.Find(s => s.TipKarte.VrstaKarte.ToString().Equals(stavka.VrstaKarte) && s.TipPopusta.VrstaPopusta.ToString().Equals(stavka.VrstaPopusta));
			}
			catch (Exception)
			{

				throw;
			}

			return result;
		}
    }
}