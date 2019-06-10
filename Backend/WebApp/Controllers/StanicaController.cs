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

		[HttpPut]
		[AllowAnonymous]
		[Route("PutStanica")]
		public IHttpActionResult PutStanica(StanicaBindingModel stanica)
		{
			var tempStanica = new Stanica()
			{
				ID = stanica.ID,
				Naziv = stanica.Naziv,
				Adresa = stanica.Adresa,
				Koordinata = new Koordinata() { X = stanica.X, Y = stanica.Y }
			};

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
					X = item.Koordinata.X,
					Y = item.Koordinata.Y,
					ID = item.ID
				});
			}

			return result;
		}

		[HttpPost]
		[AllowAnonymous]
        [Route("PostStanica")]
        public IHttpActionResult PostStanica(StanicaBindingModel novaStanica)
        {
            var stanica = new Stanica()
			{
				Naziv = novaStanica.Naziv,
				Adresa = novaStanica.Adresa,
				Koordinata = new Koordinata()
				{
					X = novaStanica.X,
					Y = novaStanica.Y
				}
			};

			try
			{
				unitOfWork.Stanice.Add(stanica);
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
