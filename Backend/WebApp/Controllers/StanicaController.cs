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

        [Route("PostStanica")]
        [Authorize(Roles ="Admin")]
        public IHttpActionResult PostStanica(StanicaBindingModel newStanica)
        {
            var stanica = new Stanica() { Naziv = newStanica.Naziv, Adresa = "Micurinova", Koordinata = new Koordinata() };


            unitOfWork.Stanice.Add(stanica);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
