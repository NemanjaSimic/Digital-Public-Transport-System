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
				if (!String.IsNullOrEmpty(TipVoznje) && item.TipLinije.ToString().Equals(TipVoznje))
				{
					names.Add(item.Ime);
				}
			}
			return names;
		}

		public void IzbrisiLiniju(string ime)
		{
			var linija = AppDbContext.Linije.ToList().FirstOrDefault(l => l.Ime.Equals(ime));
			AppDbContext.Linije.Remove(linija);
			AppDbContext.SaveChanges();
		}

		public List<Termin> GetAllTerminiOfLinija(string Ime)
		{
			return AppDbContext.Linije.ToList().Find(l => l.Ime.Equals(Ime)).Termini;
		}

		public void DodajLiniju(Linija linija)
		{
			var termini = new List<Termin>();
			foreach (var item in linija.Termini)
			{
				var termin = AppDbContext.Termini.ToList().FirstOrDefault(t => t.Dan == item.Dan && t.Polazak == item.Polazak);
				if (termin != null)
				{
					termini.Add(termin);
				}
			}
			linija.Termini = termini;
			AppDbContext.Linije.Add(linija);
			AppDbContext.SaveChanges();
		}

		public bool PosotjiLinija(string ime)
		{
			return (AppDbContext.Linije.ToList().Find(l => l.Ime.Equals(ime)) == null) ? false : true;
		}

		public Linija GetLinijaByName(string name)
		{
			return AppDbContext.Linije.ToList().FirstOrDefault(l => l.Ime.Equals(name));
		}

		public void IzmeniLiniju(Linija linija)
		{
			var tempLinija = AppDbContext.Linije.ToList().FirstOrDefault(l => l.Ime.Equals(linija.Ime));
			tempLinija.Termini.Clear();

			//foreach (var item in tempLinija.Termini)
			//{
			//	var termin = AppDbContext.Termini.ToList().FirstOrDefault(t => t.Linije.Contains(tempLinija));
			//	if (termin != null)
			//	{
			//		termini.Add(item);
			//	}
			//}

			//foreach (var item in termini)
			//{
			//	item.Linije.Remove(tempLinija);
			//	AppDbContext.SaveChanges();
			//}

			//termini = new List<Termin>();
			//foreach (var item in linija.Termini)
			//{
			//	var termin = AppDbContext.Termini.ToList().FirstOrDefault(t => t.Dan == item.Dan && t.Polazak == item.Polazak );
			//	if (termin != null)
			//	{
			//		termini.Add(termin);
			//	}
			//}

			tempLinija.RedniBroj = linija.RedniBroj;
			tempLinija.TipLinije = linija.TipLinije;
			tempLinija.Termini.AddRange(linija.Termini);
			AppDbContext.SaveChanges();
		}
	}
}