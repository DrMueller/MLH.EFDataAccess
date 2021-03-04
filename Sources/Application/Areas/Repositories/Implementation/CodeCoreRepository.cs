using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;
using Mmu.Mlh.EfDataAccess.Areas.Repositories.Base;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories.Implementation
{
    internal class CodeCoreRepository<TEntity> : CodeRepositoryBase<TEntity>
        where TEntity : CodeEntity
    {
    }
}