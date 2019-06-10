using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;

namespace WebApp.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Stanica> Stanice { get; set; }
        public DbSet<TipPopusta> TipPopustas { get; set; }
        public DbSet<TipKarte> TipKartes { get; set; }
        public DbSet<Linija> Linije { get; set; }
        public DbSet<Termin> Termini { get; set; }
        public DbSet<Karta> Karte { get; set; }
        public DbSet<Cenovnik> Cenovnici { get; set; }
        public DbSet<StavkaCenovnika> Stavke { get; set; }
        public ApplicationDbContext()
            : base("name=DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}