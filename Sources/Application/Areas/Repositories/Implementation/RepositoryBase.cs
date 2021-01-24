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
            var loadEntities = await LoadAsync(qry => qry.Where(f => f.Id.Equals(id)));
            if (loadEntities == null)
            {
                return;
            }

            _dbSet.Remove(loadEntities.Single());
        }

        public override void Initialize(IAppDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }

        //public async Task<IReadOnlyCollection<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate)
        //{
        //    var query = _dbSet.Where(predicate);
        //    var result = await query.ToListAsync();

        //    return result;
        //}

        public async Task<IReadOnlyCollection<TEntity>> LoadAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder)
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
    }
}