using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class StavkaRepository : Repository<StavkaCenovnika, int>, IStavkaRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public StavkaRepository(DbContext context) : base(context)
        {
        }
    }
}