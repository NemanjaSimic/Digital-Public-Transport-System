using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http;
using WebApp.Models;
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

		[HttpGet]
		[Authorize(Roles = "Controller")]
		[Route("Validate")]
		public string Validate(int ID)
		{
			StringBuilder result = new StringBuilder("");
			Karta karta = unitOfWork.Karte.GetKarta(ID);
			if (karta == null)
			{
				result.Append("Karta je nepostojeca.");
				return result.ToString();
			}
			else if (!ProveriKartu(karta))
			{
				result.Append("Karta je nevazeca.");
			}
			else
			{
				result.Append("Karta je vazeca.");
			}

			result.Append($"ID:{ID};");
			result.Append($"Datum i vreme izdavanja:{karta.DatumIzdavanja.ToString()};");
			result.Append($"Korisnik na koga se odnosi: {karta.Korisnik};");
			result.Append($"Tip karte: {karta.StavkaCenovnika.TipKarte.VrstaKarte.ToString()};");
			result.Append($"Vrsta karte: {karta.StavkaCenovnika.TipPopusta.VrstaPopusta.ToString()};");
			result.Append($"Cena: {karta.StavkaCenovnika.Cena.ToString()}");

			return result.ToString();
		}

		private bool ProveriKartu(Karta karta)
		{
			bool res = true;
			switch (karta.StavkaCenovnika.TipKarte.VrstaKarte)
			{
				case Models.Enums.VrstaKarte.Vremenska:
					if ((DateTime.Now - karta.DatumIzdavanja).TotalMinutes > 60)
					{
						res = false;
					}
					break;
				case Models.Enums.VrstaKarte.Dnevna:
					if (karta.DatumIzdavanja.Day != DateTime.Now.Day)
					{
						res = false;
					}
					break;
				case Models.Enums.VrstaKarte.Mesecna:
					if (karta.DatumIzdavanja.Month != DateTime.Now.Month)
					{
						res = false;
					}
					break;
				case Models.Enums.VrstaKarte.Godisnja:
					if (karta.DatumIzdavanja.Year != DateTime.Now.Year)
					{
						res = false;
					}
					break;
				default:
					break;
			}

			return res;
		}

        [HttpPost]
        [AllowAnonymous]
        [Route("PostKartaNeregistrovani")]
        public IHttpActionResult PostKartaNeregistrovani()
        {
            var req = HttpContext.Current.Request;
            var temp = req.Form.ToString();
            var email = temp.Split('-').First().Replace("%40", "@"); //don't even ask
			var idKarte = temp.Split('-').Last();

            //napravi kartu
            int id = unitOfWork.Karte.NapraviKartu("Neregistrovani korisnik", Models.Enums.VrstaKarte.Vremenska, Models.Enums.VrstaPopusta.Regular, idKarte);
            if (id != -1)
            {
                try
                {

                    MailMessage mail = new MailMessage("gulegjsp@gmail.com", email);
                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential("gulegjsp@gmail.com", "Gulice123!");
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.Host = "smtp.gmail.com";
                    mail.Subject = "Karta za autobus - GJSP Novi Sad";
                    mail.Body = $"Sifra vase karte za autobuski prevoz je -> {id}";

                    client.Send(mail);

                }
                catch (Exception)
                {
                    return BadRequest();
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
