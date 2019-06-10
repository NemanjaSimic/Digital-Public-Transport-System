using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class CenovnikRepository : Repository<Cenovnik, int>, ICenovnikRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public CenovnikRepository(DbContext context) : base(context)
        {
        }

		public List<StavkaCenovnika> GetAktuelanCenovnik()
		{
			try
			{
				return AppDbContext.Cenovnici.ToList().FirstOrDefault(c => c.Aktuelan == true).Stavke;
			}
			catch (Exception)
			{
				return new List<StavkaCenovnika>();
			}
		}

		public bool NapraviCenovnik(Cenovnik noviCenovnik)
		{
			var cenovnik = AppDbContext.Cenovnici.ToList().FirstOrDefault(c => c.Aktuelan);
			if (cenovnik != null)
			{
				cenovnik.Aktuelan = false;
				if (DateTime.Compare(cenovnik.Do,noviCenovnik.Od) > 0)
				{
					cenovnik.Do = noviCenovnik.Od;
				}
			}

			try
			{
				AppDbContext.Cenovnici.Add(noviCenovnik);
				AppDbContext.SaveChanges();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}