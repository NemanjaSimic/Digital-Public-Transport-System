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
        int Complete();
    }
}
