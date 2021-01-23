using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories.Implementation
{
    internal class CoreRepository<TEntity> : RepositoryBase<TEntity>
        where TEntity : EntityBase
    {
    }
}