using JetBrains.Annotations;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities
{
    [PublicAPI]
    public class Street : IdEntity
    {
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
    }
}