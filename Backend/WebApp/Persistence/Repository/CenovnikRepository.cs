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
			return AppDbContext.Cenovnici.ToList().Find(c => c.Aktuelan == true).Stavke;
		}
	}
}