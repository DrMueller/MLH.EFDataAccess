using System;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Entities;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;

namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetGenericRepository<TEntity>()
            where TEntity : EntityBase;

        TRepo GetRepository<TRepo>()
            where TRepo : IRepository;

        Task SaveAsync();
    }
}