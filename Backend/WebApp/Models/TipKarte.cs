using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class TipKarte
    {
        [Key]
        public VrstaKarte VrstaKarte { get; set; }
        public float CenaKarte { get; set; }
    }
}