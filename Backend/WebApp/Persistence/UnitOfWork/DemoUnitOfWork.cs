using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        [Dependency]
        public IStanicaRepository Stanice { get; set; }
        [Dependency]
        public ITipPopustaRepository TipPopustas { get; set; }
        [Dependency]
        public ITipKarteRepository TipKartas { get; set; }
        [Dependency]
        public ILinijaRepository Linije { get; set; }
        [Dependency]
        public IKartaRepository Karte { get; set; }
        [Dependency]
        public IKoordinataRepository Koordinate { get; set; }
        [Dependency]
        public ICenovnikRepository Cenovnici { get; set; }
        [Dependency]
        public IStavkaRepository Stavke { get; set; }
        [Dependency]
        public ITerminRepository Termini { get; set; }
      
        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}