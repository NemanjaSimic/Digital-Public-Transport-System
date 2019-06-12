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
	[RoutePrefix("api/Stanica")]
	public class StanicaController : ApiController
	{
		private readonly IUnitOfWork unitOfWork;

		public StanicaController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		[Route("DeleteStanica")]
		public IHttpActionResult DeleteStanica(string naziv)
		{
			var tempStanica = unitOfWork.Stanice.GetStanicaByNaziv(naziv);
			if (tempStanica == null || (tempStanica != null && tempStanica.Izbrisano))
			{
				return BadRequest($"Stanica sa nazivom {naziv} ne postoji ili je u medjuvremenu izbrisana.");
			}

			tempStanica.Izbrisano = true;
			unitOfWork.Stanice.Update(tempStanica);
			unitOfWork.Complete();

			return Ok();
		}

		[HttpPut]
		[Authorize(Roles = "Admin")]
		[Route("PutStanica")]
		public IHttpActionResult PutStanica(Stanica stanica)
		{
			var tempStanica = unitOfWork.Stanice.GetStanicaByNaziv(stanica.Naziv);
			if (tempStanica == null || (tempStanica != null && tempStanica.Izbrisano))
			{
				return BadRequest($"Stanica sa nazivom {stanica.Naziv}");
			}
			if (stanica.Verzija != tempStanica.Verzija)
			{
				return BadRequest("Stanica je izmenjena u medjuvremenu.");
			}

			tempStanica.X = stanica.X;
			tempStanica.Y = stanica.Y;
			tempStanica.Adresa = stanica.Adresa;
			tempStanica.Verzija++;

			try
			{
				unitOfWork.Stanice.Update(tempStanica);
				unitOfWork.Complete();

			}
			catch (Exception)
			{
				return BadRequest();
			}
			
			return Ok();
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("GetStanica")]
		public List<StanicaBindingModel> GetStanica()
		{
			var result = new List<StanicaBindingModel>();
			var lista = unitOfWork.Stanice.GettAllStanicaForSinglePage();
			foreach (var item in lista)
			{
				result.Add(new StanicaBindingModel()
				{
					Naziv = item.Naziv,
					Adresa = item.Adresa,
					X = item.X,
					Y = item.Y,
					Verzija = item.Verzija
				});
			}
			return result;
		}

		[HttpPost]
		[AllowAnonymous]
        [Route("PostStanica")]
        public IHttpActionResult PostStanica(Stanica novaStanica)
        {
			var stanica = unitOfWork.Stanice.GetStanicaByNaziv(novaStanica.Naziv);
			if (stanica != null)
			{
				if (stanica.Izbrisano)
				{
					unitOfWork.Stanice.Remove(stanica);
					unitOfWork.Complete();
				}
				else
				{
					return BadRequest($"Stanica sa imenom {novaStanica.Naziv} vec postoji!");
				}
			}


			novaStanica.Izbrisano = false;
			novaStanica.Verzija = 1;
			try
			{
				unitOfWork.Stanice.Add(novaStanica);
				unitOfWork.Complete();
			}
			catch (Exception)
			{
				return BadRequest();
			}
          

            return Ok();
        }
    }
}
