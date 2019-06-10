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
				if (!String.IsNullOrEmpty(TipVoznje) && item.TipLinije.ToString().Equals(TipVoznje) && !item.Izbrisano)
				{
					names.Add(item.Ime);
				}
			}
			return names;
		}

		public List<Termin> GetAllTerminiOfLinija(string Ime)
		{
			return AppDbContext.Linije.ToList().FirstOrDefault(l => l.Ime.Equals(Ime) && !l.Izbrisano).Termini;
		}

		public Linija GetLinijaByName(string name)
		{
			return AppDbContext.Linije.ToList().FirstOrDefault(l => l.Ime.Equals(name) && !l.Izbrisano);
		}

		public void IzmeniLiniju(Linija linija)
		{
			var tempLinija = AppDbContext.Linije.ToList().FirstOrDefault(l => l.Ime.Equals(linija.Ime) && !l.Izbrisano);

			tempLinija.Termini.Clear();
			tempLinija.RedniBroj = linija.RedniBroj;
			tempLinija.TipLinije = linija.TipLinije;
			tempLinija.Termini.AddRange(linija.Termini);

			AppDbContext.SaveChanges();
		}

		public Linija GetDeletedLine(string ime)
		{
			return AppDbContext.Linije.ToList().FirstOrDefault(l => l.Ime.Equals(ime) && l.Izbrisano);
		}
	}
}