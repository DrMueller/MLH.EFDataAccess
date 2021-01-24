using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        //Task<IReadOnlyCollection<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IReadOnlyCollection<TEntity>> LoadAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder);

        Task UpsertAsync(TEntity entity);
    }
}