using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class StavkaCenovnika
    {
        [Key]
        public int Id { get; set; }
        public float Cena { get { return TipKarte.CenaKarte * TipPopusta.Koeficijent; } }


        public int TipKarteId { get; set; }
        public TipKarte TipKarte { get; set; }

        public int TipPopustaId { get; set; }
        public TipPopusta TipPopusta { get; set; }

        
        public int CenovnikId { get; set; }
        public Cenovnik Cenovnik { get; set; }
    }
}