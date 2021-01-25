using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories
{
    [PublicAPI]
    public interface IRepository
    {
    }

    [PublicAPI]
    public interface IRepository<TEntity> : IRepository
        where TEntity : EntityBase
    {
        Task DeleteAsync(long id);

        Task<IReadOnlyCollection<TResult>> LoadAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder);

        Task UpsertAsync(TEntity entity);
    }
}