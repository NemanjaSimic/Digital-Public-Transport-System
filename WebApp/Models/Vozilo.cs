using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Vozilo
    {
        [Key]
        public int ID { get; set; }

        public double KordinataX { get; set; }
        public double KordinataY { get; set; }
    }
}