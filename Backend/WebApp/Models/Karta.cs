using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Karta
    {
        [Key]
        public int ID { get; set; }
        public string Korisnik { get; set; }
        public bool Validna { get; set; }
        public DateTime DatumIzdavanja { get; set; }
		public bool Izbrisano { get; set; }

		public virtual StavkaCenovnika StavkaCenovnika { get; set; }
    }
}