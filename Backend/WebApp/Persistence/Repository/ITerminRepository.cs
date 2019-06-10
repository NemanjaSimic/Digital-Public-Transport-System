using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public interface ITerminRepository : IRepository<Termin, int>
	{
		void NapraviTermin(Termin termin);
		Termin GetTermin(Dan dan, TimeSpan polazak);

	}
}
