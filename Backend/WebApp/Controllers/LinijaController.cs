using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;
using WebApp.Models.Enums;
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

		[AllowAnonymous]
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

		[AllowAnonymous]
		[Route("GetLinijeByTip")]
		public List<string> GetAllLinijeByTip(string TipVoznje)
		{
			return unitOfWork.Linije.GetAllLinijaNamesByTip(TipVoznje);
		}

		[AllowAnonymous]
		[Route("GetTerminiOfLinija")]
		public List<string> GetTerminiOfLinija(string Ime,string Dan)
		{
			List<string> retVal = new List<string>();
			try
			{
				foreach (var item in unitOfWork.Linije.GetAllTerminiOfLinija(Ime))
				{
					if (String.IsNullOrEmpty(Dan))
					{
						retVal.Add(item.Polazak.ToString());
					}
					else if (item.Dan.ToString().Equals(Dan))
					{
						retVal.Add(item.Polazak.ToString());

					}
					retVal.Sort();
				}
			}
			catch (Exception)
			{

				throw;
			}
			return retVal.Distinct().ToList();
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("PostLinija")]
		public IHttpActionResult PostLinija(NovaLinijaBindingModel novaLinija)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			
			if (unitOfWork.Linije.PosotjiLinija(novaLinija.Ime))
			{
				return BadRequest("Ime linije vec postoji");
			}

			var linija = new Linija()
			{
				Ime = novaLinija.Ime,
				RedniBroj = novaLinija.RedniBroj,
				TipLinije = (VrstaLinije)Enum.Parse(typeof(VrstaLinije), novaLinija.VrstaLinije),
				Termini = new List<Termin>(),
				Stanice = new List<Stanica>()
			};

			try
			{
				linija.Termini.AddRange(ConvertToTermins(novaLinija.RadniDanTermini, Dan.RadniDan));
				linija.Termini.AddRange(ConvertToTermins(novaLinija.SubotaTermini, Dan.Subota));
				linija.Termini.AddRange(ConvertToTermins(novaLinija.NedeljaTermini, Dan.Nedelja));
			}
			catch (Exception)
			{
				return BadRequest("Format polazaka je los.");
			}
			

			unitOfWork.Linije.DodajLiniju(linija);

			return Ok();
		}

		private List<Termin> ConvertToTermins(List<string> list, Dan dan)
		{
			var retVal = new List<Termin>();
			foreach (var item in list)
			{
				if (!String.IsNullOrEmpty(item))
				{
					var termin = new Termin() { Dan = dan, Polazak = TimeSpan.Parse(item), Linije = new List<Linija>()  };
					unitOfWork.Termini.NapraviTermin(termin);
					retVal.Add(termin);
				}
			}

			return retVal;
		}
    }
}
