﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

		[ForeignKey("StavkaCenovnika")]
        public int StavkaCenovnikaId { get; set; }
        public StavkaCenovnika StavkaCenovnika { get; set; }
    }
}