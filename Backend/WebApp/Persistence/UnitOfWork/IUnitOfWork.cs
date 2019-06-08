using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IStanicaRepository Stanice { get; set; }
        ITipPopustaRepository TipPopustas { get; set; }
        ITipKarteRepository TipKartas { get; set; }
        ILinijaRepository Linije { get; set; }
        IKartaRepository Karte { get; set; }
        IKoordinataRepository Koordinate { get; set; }
        ICenovnikRepository Cenovnici { get; set; }
        IStavkaRepository Stavke { get; set; }
        ITerminRepository Termini { get; set; }
        int Complete();
    }
}
