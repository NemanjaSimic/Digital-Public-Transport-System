using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class StavkaCenovnika
    {
        [Key]
        public int Id { get; set; }
		public float Cena { get; set; }

		[ForeignKey("TipKarte")]
        public VrstaKarte TipKarteId { get; set; }
        public TipKarte TipKarte { get; set; }

		[ForeignKey("TipPopusta")]
		public VrstaPopusta TipPopustaId { get; set; }
        public TipPopusta TipPopusta { get; set; }

		[ForeignKey("Cenovnik")]
		public int CenovnikId { get; set; }
        public Cenovnik Cenovnik { get; set; }
    }
}