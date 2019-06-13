using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Hubs;

namespace WebApp.Controllers
{
    public class VoziloController : ApiController
    {
		private LocationHub hub;

		public VoziloController(LocationHub hub)
		{
			this.hub = hub;
		}


    }
}
