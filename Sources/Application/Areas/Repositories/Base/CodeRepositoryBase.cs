using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories.Base
{
    public abstract class CodeRepositoryBase<TEntity> : RepositoryBase<TEntity>, ICodeRepository<TEntity>
        where TEntity : CodeEntity
    {
        public async Task DeleteAsync(string code)
        {
            var loadedEntity = await LoadAsync(qry => qry.SingleOrDefaultAsync(f => f.Code.Equals(code)));

            if (loadedEntity == null)
            {
                return;
            }

            DbSet.Remove(loadedEntity);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IReadOnlyCollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                await InsertAsync(entity);
            }
        }
    }
}