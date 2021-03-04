using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mmu.Mlh.EfDataAccess.Areas.Data.TypeConfigurations.Base;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.TypeConfigurations
{
    public class CurrencyConfig : CodeEntityConfigBase<Currency>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Currency> builder)
        {
        }
    }
}