using System;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Entities;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;

namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        // Technically, we could merge these two, but then there is either a superfluous generic parameter sometimes or they need to request IRepository<Individual> etc.
        // Let's say with two methods
        IRepository<TEntity> GetGenericRepository<TEntity>()
            where TEntity : EntityBase;

        TRepo GetRepository<TRepo>()
            where TRepo : IRepository;

        Task SaveAsync();
    }
}