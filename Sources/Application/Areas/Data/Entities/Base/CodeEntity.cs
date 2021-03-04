using JetBrains.Annotations;

namespace Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base
{
    [PublicAPI]
    public abstract class CodeEntity : EntityBase
    {
        public string Code { get; set; }
    }
}