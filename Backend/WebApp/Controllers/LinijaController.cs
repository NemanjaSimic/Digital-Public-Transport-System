using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
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

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		[Route("DeleteLinija")]
		public IHttpActionResult DeleteLinija(string ime)
		{

			var linija = unitOfWork.Linije.GetLinijaByName(ime);
			if (linija == null)
			{
				return BadRequest("Linija ne postoji u bazi.");
			}

			linija.Izbrisano = true;
			unitOfWork.Linije.Update(linija);
			unitOfWork.Complete();
			return Ok();

		}

		[HttpGet]
		[AllowAnonymous]
		[ResponseType(typeof(NovaLinijaBindingModel))]
		[Route("GetLinija")]
		public IHttpActionResult GetLinija(string name)
		{
			Linija linija = unitOfWork.Linije.GetLinijaByName(name);

			if (linija == null)
			{
				return BadRequest();
			}

			var novaLinija = new NovaLinijaBindingModel()
			{
				Ime = linija.Ime,
				RedniBroj = linija.RedniBroj,
				VrstaLinije = linija.TipLinije.ToString(),
				RadniDanTermini = new List<string>(),
				SubotaTermini = new List<string>(),
				NedeljaTermini = new List<string>(),
				Stanice = new List<string>()
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

			foreach (var item in linija.Stanice)
			{
				novaLinija.Stanice.Add(item.Naziv);
			}

			return Ok(novaLinija);
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
		[Authorize(Roles = "Admin")]
		[Route("PutLinija")]
		public IHttpActionResult PutLinija(NovaLinijaBindingModel novaLinija)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Linija linija = unitOfWork.Linije.GetLinijaByName(novaLinija.Ime);
			if (linija == null)
			{
				return BadRequest($"Linija sa imenom {novaLinija.Ime} ne postoji.");

			}

			linija.RedniBroj = novaLinija.RedniBroj;
			linija.TipLinije = (VrstaLinije)Enum.Parse(typeof(VrstaLinije), novaLinija.VrstaLinije);
			linija.Termini.Clear();
			linija.Stanice.Clear();

			try
			{
				linija.Termini.AddRange(ConvertToTermins(novaLinija.RadniDanTermini, Dan.RadniDan));
				linija.Termini.AddRange(ConvertToTermins(novaLinija.SubotaTermini, Dan.Subota));
				linija.Termini.AddRange(ConvertToTermins(novaLinija.NedeljaTermini, Dan.Nedelja));

				foreach (var item in novaLinija.Stanice)
				{
					var stanica = unitOfWork.Stanice.GetStanicaByNaziv(item);
					if (stanica != null)
					{
						linija.Stanice.Add(stanica);
					}
				}
			}
			catch (Exception)
			{
				return BadRequest("Format polazaka je los.");
			}

			unitOfWork.Linije.Update(linija);
			unitOfWork.Complete();

			return Ok();
		}



		[HttpPost]
		[Authorize(Roles = "Admin")]
		[Route("PostLinija")]
		public IHttpActionResult PostLinija(NovaLinijaBindingModel novaLinija)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Linija linija = unitOfWork.Linije.GetLinijaByName(novaLinija.Ime);
			
			if (linija != null)
			{			
				return BadRequest($"Linija sa imenom {novaLinija.Ime} vec postoji.");
			}
			else
			{
				linija = unitOfWork.Linije.GetDeletedLine(novaLinija.Ime);
				if (linija != null)
				{
					unitOfWork.Linije.Remove(linija);
					unitOfWork.Complete();
				}
				
			}

			linija = new Linija()
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
				foreach (var item in novaLinija.Stanice)
				{
					var stanica = unitOfWork.Stanice.GetStanicaByNaziv(item);
					if (stanica != null)
					{
						linija.Stanice.Add(stanica);
					}
				}
			}
			catch (Exception)
			{
				return BadRequest("Format polazaka je los.");
			}


			unitOfWork.Linije.Add(linija);
			unitOfWork.Complete();

			return Ok();
		}

		private List<Termin> ConvertToTermins(List<string> list, Dan dan)
		{
			var retVal = new List<Termin>();
			foreach (var item in list)
			{
				if (!String.IsNullOrEmpty(item))
				{
					Termin terminTemp = unitOfWork.Termini.GetTermin(dan,TimeSpan.Parse(item));
					Termin termin;
					if (terminTemp == null)
					{
						termin = new Termin() { Dan = dan, Polazak = TimeSpan.Parse(item), Linije = new List<Linija>()  };
						unitOfWork.Termini.Add(termin);
						unitOfWork.Complete();
						retVal.Add(termin);
					}
					else
					{
						retVal.Add(terminTemp);
					}
				}
			}
			return retVal;
		}
    }
}
