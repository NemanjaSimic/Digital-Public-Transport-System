﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class Linija
    {
        [Key]
        public int ID { get; set; }
        public VrstaLinije TipLinije { get; set; }
        public string Ime { get; set; }
        public int RedniBroj { get; set; }
        public List<Termin> Termini { get; set; }
        public List<Stanica> Stanice { get; set; }
    }
}