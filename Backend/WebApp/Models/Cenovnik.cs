using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class Cenovnik
    {
        [Key]
        public int ID { get; set; }
        public DateTime Od { get; set; }
        public DateTime Do { get; set; }


        [InverseProperty("Cenovnik")]
        public virtual List<StavkaCenovnika> Stavke { get; set; }
    }
}