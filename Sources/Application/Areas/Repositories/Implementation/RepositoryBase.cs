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
        private IAppDbContext _dbContext;
        private DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();
        protected IQueryable<TEntity> Query => DbSet;

        public async Task DeleteAsync(long id)
        {
            var loadEntities = await LoadAsync(qry => qry.Where(f => f.Id.Equals(id)));
            if (loadEntities == null)
            {
                return;
            }

            DbSet.Remove(loadEntities.Single());
        }

        public async Task<IReadOnlyCollection<TResult>> LoadAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder)
        {
            var qry = queryBuilder(DbSet);
            var lst = await qry.ToListAsync();

            return lst;
        }

        public async Task<TResult> LoadAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> queryBuilder)
        {
            var qry = await queryBuilder(DbSet);

            return qry;
        }

        public async Task UpsertAsync(TEntity entity)
        {
            if (entity.Id.Equals(default))
            {
                await DbSet.AddAsync(entity);
            }
            else
            {
                var attachedEntity = _dbContext.ChangeTracker.Entries<TEntity>().SingleOrDefault(e => e.Entity.Id == entity.Id);
                if (attachedEntity == null)
                {
                    DbSet.Update(entity);
                }
            }
        }

        void IRepositoryBase.Initialize(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}