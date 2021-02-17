using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.Areas.Querying.Implementation
{
    internal class QueryService : IQueryService
    {
        private readonly IAppDbContextFactory _appDbContextFactory;

        public QueryService(IAppDbContextFactory appDbContextFactory)
        {
            _appDbContextFactory = appDbContextFactory;
        }

        public async Task<TResult> QueryAsync<TEntity, TResult>(Func<IQueryable<TEntity>, Task<TResult>> queryBuilder)
            where TEntity : EntityBase
        {
            using var appDbContext = _appDbContextFactory.Create();
            var dbSet = appDbContext.Set<TEntity>().AsNoTracking();
            var result = await queryBuilder(dbSet);

            return result;
        }

        public async Task<TEntity> QuerySingleAsync<TEntity>(Func<IQueryable<TEntity>, Task<TEntity>> queryBuilder)
            where TEntity : EntityBase
        {
            using var appDbContext = _appDbContextFactory.Create();
            var dbSet = appDbContext.Set<TEntity>().AsNoTracking();
            var result = await queryBuilder(dbSet);

            return result;
        }
    }
}