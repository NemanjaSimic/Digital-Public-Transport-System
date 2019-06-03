using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class LinijaRepository : Repository<Linija, int>, ILinijaRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public LinijaRepository(DbContext context) : base(context)
        {
        }
    }
}