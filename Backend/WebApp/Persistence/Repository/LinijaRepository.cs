using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
	public class LinijaRepository : Repository<Linija, int>, ILinijaRepository
	{
		protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
		public LinijaRepository(DbContext context) : base(context)
		{

		}

		public List<string> GetAllLinijaNamesByTip(string TipVoznje)
		{
			List<string> names = new List<string>();
			foreach (var item in AppDbContext.Linije)
			{
				if (!String.IsNullOrEmpty(TipVoznje) &&  item.TipLinije.ToString().Equals(TipVoznje))
				{
					names.Add(item.Ime);
				}
			}
			return names;
		}

		public List<Termin> GetAllTerminiOfLinija(string Ime)
		{
			return AppDbContext.Linije.ToList().Find(l => l.Ime.Equals(Ime)).Termini;
		}

		public void DodajLiniju(Linija linija)
		{
			AppDbContext.Linije.Add(linija);
			AppDbContext.SaveChanges();
		}

		public bool PosotjiLinija(string ime)
		{
			return (AppDbContext.Linije.ToList().Find(l => l.Ime.Equals(ime)) == null) ? false : true;
		}
	}
}