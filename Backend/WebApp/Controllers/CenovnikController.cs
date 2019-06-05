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
	[RoutePrefix("api/Cenovnik")]
	public class CenovnikController : ApiController
	{
		private readonly IUnitOfWork unitOfWork;
		public CenovnikController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		[Route("GetCenovnik")]
		public List<StavkaBindingModel> GetStavkeCenovnika()
		{
			List<StavkaBindingModel> retVal = new List<StavkaBindingModel>();

			foreach (var item in unitOfWork.Cenovnici.GetAktuelanCenovnik())
			{
				retVal.Add(new StavkaBindingModel()
				{
					Cena = item.Cena,
					VrstaKarte = item.TipKarteId.ToString(),
					VrstaPopusta = item.TipPopustaId.ToString()
				});
			} 

			return retVal;
		}
	}
}
