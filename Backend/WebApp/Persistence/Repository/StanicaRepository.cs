using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class StanicaRepository : Repository<Stanica, int>, IStanicaRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public StanicaRepository(DbContext context) : base (context)
        {

        }

        public List<Stanica> GettAllStanicaForSinglePage()
        {
            return AppDbContext.Stanice.ToList();
        }
    }
}