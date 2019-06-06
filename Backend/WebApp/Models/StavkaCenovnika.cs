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

		//public VrstaKarte TipKarteId { get; set; }
		public virtual TipKarte TipKarte { get; set; }

		//public VrstaPopusta TipPopustaId { get; set; }
        public virtual TipPopusta TipPopusta { get; set; }

		//public int CenovnikId { get; set; }
		public virtual Cenovnik Cenovnik { get; set; }
    }
}