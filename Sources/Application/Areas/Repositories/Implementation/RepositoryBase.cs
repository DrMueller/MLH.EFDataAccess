using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories.Implementation
{
    // just to make it easy to initialize it
    internal interface IRepositoryBase
    {
        internal void Initialize(IAppDbContext dbContext);
    }

    public abstract class RepositoryBase<TEntity> : IRepositoryBase, IRepository<TEntity>
        where TEntity : EntityBase
    {
        private DbSet<TEntity> _dbSet;
        protected IQueryable<TEntity> Query => _dbSet;

        public async Task DeleteAsync(long id)
        {
            var loadEntities = await LoadAsync(qry => qry.Where(f => f.Id.Equals(id)));
            if (loadEntities == null)
            {
                return;
            }

            _dbSet.Remove(loadEntities.Single());
        }

        public async Task<IReadOnlyCollection<TResult>> LoadAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder)
        {
            var qry = queryBuilder(_dbSet);
            var lst = await qry.ToListAsync();

            return lst;
        }

        public async Task UpsertAsync(TEntity entity)
        {
            if (entity.Id.Equals(default))
            {
                await _dbSet.AddAsync(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }
        }

        void IRepositoryBase.Initialize(IAppDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }
    }
}