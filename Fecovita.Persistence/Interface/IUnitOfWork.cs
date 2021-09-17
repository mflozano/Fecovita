using Fecovita.Core.Entities.Base;
using Fecovita.Core.IRepository;
using Fecovita.Core.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fecovita.Persistence.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        FecovitaContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
        IRepository<T> Repository<T>() where T : BaseEntity;
        IProductRepository ProductRepository { get; }       
    }
}
