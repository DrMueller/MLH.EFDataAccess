using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories.Base
{
    public abstract class IdRepositoryBase<TEntity> : RepositoryBase<TEntity>, IIdRepository<TEntity>
        where TEntity : IdEntity
    {
        public async Task DeleteAsync(long id)
        {
            var loadedEntity = await LoadAsync(qry => qry.SingleOrDefaultAsync(f => f.Id.Equals(id)));

            if (loadedEntity == null)
            {
                return;
            }

            DbSet.Remove(loadedEntity);
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

        public async Task UpsertRangeAsync(IReadOnlyCollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                await UpsertAsync(entity);
            }
        }
    }
}