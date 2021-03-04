using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mmu.Mlh.EfDataAccess.Areas.Data.TypeConfigurations.Base;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.TypeConfigurations
{
    public class StreetConfig : IdEntityConfigBase<Street>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Street> builder)
        {
            builder.Property(f => f.StreetName).IsRequired();
            builder.Property(f => f.StreetNumber).IsRequired();

            builder.ToTable("Street", "Core");
        }
    }
}