using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Karta
    {
        [Key]
        public int ID { get; set; }
        public int TipKarteId { get; set; }
        public TipKarte TipKarte { get; set; }
        public int TipPopustaId { get; set; }
        public TipPopusta TipPopusta { get; set; }
        public float Cena { get; set; }
        public bool Validna { get; set; }
        public DateTime DatumIzdavanja { get; set; }
    }
}