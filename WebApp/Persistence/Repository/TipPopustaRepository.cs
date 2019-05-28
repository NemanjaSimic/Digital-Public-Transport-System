using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Enums;

namespace WebApp.Persistence.Repository
{
    public class TipPopustaRepository : Repository<TipPopusta, VrstaPopusta>, ITipPopustaRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public TipPopustaRepository(DbContext context):base(context)
        {

        }
    }
}