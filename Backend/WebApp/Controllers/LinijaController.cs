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

		[HttpGet]
		[AllowAnonymous]
		[Route("GetLinija")]
		public NovaLinijaBindingModel GetLinija(string name)
		{
			Linija linija;
			try
			{
				linija = unitOfWork.Linije.GetLinijaByName(name);
			}
			catch (Exception)
			{

				throw;
			} 

			var novaLinija = new NovaLinijaBindingModel()
			{
				Ime = linija.Ime,
				RedniBroj = linija.RedniBroj,
				VrstaLinije = linija.TipLinije.ToString(),
				RadniDanTermini = new List<string>(),
				SubotaTermini = new List<string>(),
				NedeljaTermini = new List<string>()
			};

			foreach (var item in linija.Termini)
			{
				switch (item.Dan)
				{
					case Dan.RadniDan:
						novaLinija.RadniDanTermini.Add(item.Polazak.ToString());
						break;
					case Dan.Subota:
						novaLinija.SubotaTermini.Add(item.Polazak.ToString());
						break;
					case Dan.Nedelja:
						novaLinija.NedeljaTermini.Add(item.Polazak.ToString());
						break;
					default:
						break;
				}
			}

			return novaLinija;
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
		[HttpPut]
		[AllowAnonymous]
		[Route("PutLinija")]
		public IHttpActionResult PutLinija(NovaLinijaBindingModel novaLinija)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}


			if (!unitOfWork.Linije.PosotjiLinija(novaLinija.Ime))
			{
				return BadRequest("Linija ne postoji");
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

			try
			{
				unitOfWork.Linije.IzmeniLiniju(linija);
			}
			catch (Exception)
			{
				return BadRequest();
			}

			return Ok();
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
