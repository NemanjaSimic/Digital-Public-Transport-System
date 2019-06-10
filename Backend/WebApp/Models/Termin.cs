using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class Termin
    {
        public int Id { get; set; }
        public Dan Dan { get; set; }
		public TimeSpan Polazak { get; set; }

        public virtual List<Linija> Linije { get; set; }
    }
}