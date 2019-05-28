using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Enums;

namespace WebApp.Models
{
    public class TipPopusta
    {
        public VrstaPopusta VrstaPopusta { get; set; }
        public float Koeficijent { get; set; }
    }
}