using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories.Implementation
{
    public abstract class RepositoryBase
    {
        public abstract void Initialize(IAppDbContext dbContext);
    }

    public abstract class RepositoryBase<TEntity> : RepositoryBase, IRepository<TEntity>
        where TEntity : EntityBase
    {
        private DbSet<TEntity> _dbSet;

        protected IQueryable<TEntity> Query => _dbSet;
        
        public async Task DeleteAsync(long id)
        {
            var entity = await LoadSingleAsync(f => f.Id.Equals(id));
            if (entity == null)
            {
                return;
            }

            _dbSet.Remove(entity);
        }

        public override void Initialize(IAppDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<IReadOnlyCollection<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbSet.Where(predicate);

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<TEntity> LoadSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var lst = await LoadAsync(predicate);

            return lst.Single();
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
    }
}