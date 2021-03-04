using System;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;

namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        ICodeRepository<TEntity> GetCodeRepository<TEntity>()
            where TEntity : CodeEntity;

        IIdRepository<TEntity> GetIdRepository<TEntity>()
            where TEntity : IdEntity;

        TRepo GetRepository<TRepo>()
            where TRepo : IRepository;

        Task SaveAsync();
    }
}