using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories
{
    [PublicAPI]
    public interface IIdRepository<TIdEntity> : IRepository<TIdEntity>
        where TIdEntity : IdEntity
    {
        Task DeleteAsync(long id);

        Task UpsertAsync(TIdEntity entity);

        Task UpsertRangeAsync(IReadOnlyCollection<TIdEntity> entities);
    }
}