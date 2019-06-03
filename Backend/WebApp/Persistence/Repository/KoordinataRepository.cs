﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class KoordinataRepository : Repository<Koordinata, int>, IKoordinataRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public KoordinataRepository(DbContext context) : base(context)
        {
        }
    }
}