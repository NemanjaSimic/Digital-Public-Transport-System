using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class Termin
    {
        [Key]
        public int ID { get; set; }
        public Dan Dan { get; set; }
        public DateTime Polazak { get; set; }

        public virtual List<Linija> Linije { get; set; }
    }
}