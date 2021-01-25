using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities.TypeConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasMany(f => f.Streets).WithOne().IsRequired();
            builder.ToTable("Address", "Core");
        }
    }
}