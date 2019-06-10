using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
	[RoutePrefix("api/Cenovnik")]
	public class CenovnikController : ApiController
	{
		private readonly IUnitOfWork unitOfWork;
		public CenovnikController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		[HttpGet]
		[AllowAnonymous]
		[ResponseType(typeof(List<StavkaBindingModel>))]
		[Route("GetCenovnik")]
		public IHttpActionResult GetStavkeCenovnika()
		{
			List<StavkaBindingModel> retVal = new List<StavkaBindingModel>();
			var stavke = unitOfWork.Cenovnici.GetAktuelanCenovnik();
			if (stavke != null)
			{
				foreach (var item in stavke)
				{
					retVal.Add(new StavkaBindingModel()
					{
						Cena = item.Cena,
						VrstaKarte = item.TipKarte.VrstaKarte.ToString(),
						VrstaPopusta = item.TipPopusta.VrstaPopusta.ToString()
					});
				}
			}
			return Ok(retVal);
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[Route("NapraviCenovnik")]
		public IHttpActionResult NapraviCenovnik(NoviCenovnikBindingModel noviCenovnik)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var cenovnik = new Cenovnik() { Od = DateTime.Now, Do = noviCenovnik.Do, Aktuelan = true, Stavke = new List<StavkaCenovnika>() };
				unitOfWork.Cenovnici.NapraviCenovnik(cenovnik);
			
				var tkarta1 = new TipKarte() { CenaKarte = noviCenovnik.Godisnja, VrstaKarte = Models.Enums.VrstaKarte.Godisnja };
				var tkarta2 = new TipKarte() { CenaKarte = noviCenovnik.Vremenska, VrstaKarte = Models.Enums.VrstaKarte.Vremenska };
				var tkarta3 = new TipKarte() { CenaKarte = noviCenovnik.Dnevna, VrstaKarte = Models.Enums.VrstaKarte.Dnevna };
				var tkarta4 = new TipKarte() { CenaKarte = noviCenovnik.Mesecna, VrstaKarte = Models.Enums.VrstaKarte.Mesecna };

				unitOfWork.TipKartas.DodajTipKarte(tkarta1);
				unitOfWork.TipKartas.DodajTipKarte(tkarta2);
				unitOfWork.TipKartas.DodajTipKarte(tkarta3);
				unitOfWork.TipKartas.DodajTipKarte(tkarta4);


				unitOfWork.Stavke.NovaStavkaSaSvimPopustima(tkarta1, cenovnik);
				unitOfWork.Stavke.NovaStavkaSaSvimPopustima(tkarta2, cenovnik);
				unitOfWork.Stavke.NovaStavkaSaSvimPopustima(tkarta3, cenovnik);
				unitOfWork.Stavke.NovaStavkaSaSvimPopustima(tkarta4, cenovnik);
			}
			catch (Exception)
			{
				return BadRequest();
				throw;
			}

			return Ok();
		}
	}
}
