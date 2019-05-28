using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class TipPopusta
    {
        [Key]
        public VrstaPopusta VrstaPopusta { get; set; }
        public float Koeficijent { get; set; }
    }
}