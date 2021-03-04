using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories
{
    [PublicAPI]
    public interface IRepository
    {
    }

    [PublicAPI]
    public interface IRepository<out TEntity> : IRepository
        where TEntity : EntityBase
    {
        Task<TResult> LoadAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> queryBuilder);
    }
}