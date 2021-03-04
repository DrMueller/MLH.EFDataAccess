using JetBrains.Annotations;

namespace Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base
{
    [PublicAPI]
    public abstract class IdEntity : EntityBase
    {
        public long Id { get; set; }
    }
}