using JetBrains.Annotations;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities
{
    [PublicAPI]
    public class Currency : CodeEntity
    {
        public string Description { get; set; }
    }
}