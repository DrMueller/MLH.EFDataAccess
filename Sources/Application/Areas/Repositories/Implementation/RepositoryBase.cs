using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories.Implementation
{
    // We can't currently cast to generic types, so we use a ungeneric one for easeness
    public abstract class RepositoryBase
    {
        public abstract void Initialize(IDbContext dbContext);
    }

    public abstract class RepositoryBase<TEntity> : RepositoryBase
        where TEntity : EntityBase
    {
        protected DbSet<TEntity> DbSet { get; private set; }

        public override void Initialize(IDbContext dbContext)
        {
            DbSet = dbContext.Set<TEntity>();
        }

        public async Task<IReadOnlyCollection<TEntity>> LoadAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task UpsertAsync(TEntity entity)
        {
            if (entity.Id.Equals(default))
            {
                await DbSet.AddAsync(entity);
            }
            else
            {
                DbSet.Update(entity);
            }
        }
    }
}