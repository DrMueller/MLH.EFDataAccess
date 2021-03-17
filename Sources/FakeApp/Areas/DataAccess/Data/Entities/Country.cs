using JetBrains.Annotations;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities
{
    [PublicAPI]
    public class Country : CodeEntity
    {
        public Currency Currency { get; set; }

        public string CurrencyCode { get; set; }
        public string Name { get; set; }
    }
}