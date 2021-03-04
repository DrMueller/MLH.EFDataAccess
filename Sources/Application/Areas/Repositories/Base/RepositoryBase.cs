using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts.Contexts;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories.Base
{
    internal interface IRepositoryBase
    {
        internal void Initialize(IAppDbContext dbContext);
    }

    public abstract class RepositoryBase<TEntity> : IRepositoryBase
        where TEntity : EntityBase
    {
        internal DbSet<TEntity> DbSet { get; private set; }
        protected IQueryable<TEntity> Query => DbSet;

        public async Task<TResult> LoadAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> queryBuilder)
        {
            var qry = await queryBuilder(DbSet);

            return qry;
        }

        void IRepositoryBase.Initialize(IAppDbContext dbContext)
        {
            DbSet = dbContext.Set<TEntity>();
        }
    }
}