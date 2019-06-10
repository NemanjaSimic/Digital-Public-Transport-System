using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Stanica
    {
        [Key]
        public string Naziv { get; set; }
        public string Adresa { get; set; }
		public bool Izbrisano { get; set; }
		public double X { get; set; }
		public double Y { get; set; }

        public virtual List<Linija> Linije { get; set; }
    }
}