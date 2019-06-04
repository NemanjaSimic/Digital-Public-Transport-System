using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
	[RoutePrefix("api/Linija")]
	public class LinijaController : ApiController
    {
		private readonly IUnitOfWork unitOfWork;
		public LinijaController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		[Route("GetLinija")]
		public Linija GetLinija()
		{
			return new Linija() {
				ID = 1,
				Ime = "Liman",
				TipLinije = Models.Enums.VrstaLinije.Gradski,
				RedniBroj = 4,
				Termini = new List<Termin>(),
				Stanice = new List<Stanica>()
			};
		}
		[Route("GetLinijeByTip")]
		public List<string> GetAllLinijeByTip(string TipVoznje)
		{
			return unitOfWork.Linije.GetAllLinijaNamesByTip(TipVoznje);
		}

		[Route("GetTerminiOfLinija")]
		public List<string> GetTerminiOfLinija(string Ime,string Dan)
		{
			List<string> retVal = new List<string>();
			foreach (var item in unitOfWork.Linije.GetAllTerminiOfLinija(Ime))
			{
				if (String.IsNullOrEmpty(Dan))
				{
					retVal.Add(item.Polazak.ToString());
				}
				else if(item.Dan.ToString().Equals(Dan))
				{
					retVal.Add(item.Polazak.ToString());

				}
				retVal.Sort();
			}
			return retVal.Distinct().ToList();
		}
    }
}
