using JetBrains.Annotations;

namespace Mmu.Mlh.EfDataAccess.Areas.Entities
{
    [PublicAPI]
    public abstract class EntityBase
    {
        public long Id { get; set; }
    }
}