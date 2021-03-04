using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mmu.Mlh.EfDataAccess.Areas.Data.TypeConfigurations.Base;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.TypeConfigurations
{
    public class AddressConfig : IdEntityConfigBase<Address>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Address> builder)
        {
            builder.HasMany(f => f.Streets).WithOne().IsRequired();
            builder.ToTable("Address", "Core");
        }
    }
}