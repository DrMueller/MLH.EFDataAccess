using System;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.Areas.Querying
{
    public interface IQueryService
    {
        Task<TResult> QueryAsync<TEntity, TResult>(Func<IQueryable<TEntity>, Task<TResult>> queryBuilder)
            where TEntity : EntityBase;

        Task<TEntity> QuerySingleAsync<TEntity>(Func<IQueryable<TEntity>, Task<TEntity>> queryBuilder)
            where TEntity : EntityBase;
    }
}