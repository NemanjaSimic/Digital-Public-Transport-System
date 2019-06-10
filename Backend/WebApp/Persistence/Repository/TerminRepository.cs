using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class TerminRepository : Repository<Termin, int>, ITerminRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public TerminRepository(DbContext context) : base(context)
        {
        }

		public Termin GetTermin(Dan dan, TimeSpan polazak)
		{
			return AppDbContext.Termini.ToList().FirstOrDefault(t => t.Dan == dan && t.Polazak == polazak);
		}
		public void NapraviTermin(Termin termin)
		{
			var tempTermin = AppDbContext.Termini.ToList().Find(t => t.Dan == termin.Dan && t.Polazak == termin.Polazak);
			if (tempTermin == null)
			{
				AppDbContext.Termini.Add(termin);
				AppDbContext.SaveChanges();
			}
		}
    }
}