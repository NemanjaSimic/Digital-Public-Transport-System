using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class Linija
    {
		[Key]
		public string Ime { get; set; }
        public VrstaLinije TipLinije { get; set; }
        public int RedniBroj { get; set; }
		public bool Izbrisano { get; set; }

		public virtual List<Termin> Termini { get; set; }

        public virtual List<Stanica> Stanice { get; set; }
    }
}