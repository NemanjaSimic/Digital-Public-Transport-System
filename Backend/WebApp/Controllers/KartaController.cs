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


        [HttpPost]
        [AllowAnonymous]
        [Route("PostKarta")]
        public IHttpActionResult PostKarta()
        {
            var req = HttpContext.Current.Request;
            var temp = req.Form.ToString();
            var email = temp.Replace("%40", "@");

            //napravi kartu
            int id = unitOfWork.Karte.NapraviKartu(Models.Enums.VrstaKarte.Vremenska, Models.Enums.VrstaPopusta.Regular);
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


                    //Send the msg
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
