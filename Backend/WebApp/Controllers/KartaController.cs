using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
	[RoutePrefix("api/Karta")]
    public class KartaController : ApiController
    {
		private readonly IUnitOfWork unitOfWork;
		public KartaController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}


		[Route("PostNeregKarta")]
		public IHttpActionResult PostNeregKarta()
		{
			var req = HttpContext.Current.Request;
			var email = req.Form["email"];
			int id = unitOfWork.Karte.NapraviKartu(Models.Enums.VrstaKarte.Vremenska, Models.Enums.VrstaPopusta.Regularna);
			if (id != -1)
			{
				MailMessage mail = new MailMessage();
				SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
				mail.From = new MailAddress("simke966@gmail.com");
				mail.To.Add(email);
				mail.Subject = "how to send email via a webapi";
				mail.Body = "sending emails can be fun and very profitable.";
				SmtpClient client = new SmtpClient();
				client.DeliveryMethod = SmtpDeliveryMethod.Network;

				client.Host = "relay-hosting.secureserver.net";
				client.Port = 25;

	

				//Send the msg
				client.Send(mail);
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}

    }
}
