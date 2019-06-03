using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Stanica
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public int KoordinataId { get; set; }
        public Koordinata Koordinata { get; set; }

        public virtual List<Linija> Linije { get; set; }
    }
}