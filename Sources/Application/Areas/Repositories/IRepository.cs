using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity> : IRepository
        where TEntity : EntityBase
    {
        Task DeleteAsync(long id);

        Task<IReadOnlyCollection<TResult>> LoadAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder);

        Task UpsertAsync(TEntity entity);
    }
}