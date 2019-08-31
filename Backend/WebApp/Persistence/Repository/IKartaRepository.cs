using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public interface IKartaRepository : IRepository<Karta, int>
	{
		int NapraviKartu(string korisnik, VrstaKarte vrstaKarte, VrstaPopusta vrstaPopusta, string id);
		Karta GetKarta(int ID);

	}
}
