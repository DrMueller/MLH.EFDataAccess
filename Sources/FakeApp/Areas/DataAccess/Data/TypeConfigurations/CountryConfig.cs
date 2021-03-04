using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mmu.Mlh.EfDataAccess.Areas.Data.TypeConfigurations.Base;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.TypeConfigurations
{
    public class CountryConfig : CodeEntityConfigBase<Country>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Country> builder)
        {
            builder.HasOne(f => f.Currency)
                .WithOne()
                .HasForeignKey<Country>(f => f.CurrencyCode);

            builder.ToTable("Country", "Core");
        }
    }
}