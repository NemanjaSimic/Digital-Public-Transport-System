﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public interface ILinijaRepository : IRepository<Linija, int>
	{
		List<string> GetAllLinijaNamesByTip(string TipVoznje);
		List<Termin> GetAllTerminiOfLinija(string Ime);
		Linija GetLinijaByName(string name);
		void IzmeniLiniju(Linija linija);
		Linija GetDeletedLine(string ime);
	}

}
