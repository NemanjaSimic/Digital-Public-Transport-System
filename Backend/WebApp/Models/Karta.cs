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
        public bool Validna { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public int StavkaCenovnikaId { get; set; }
        public StavkaCenovnika StavkaCenovnika { get; set; }
    }
}