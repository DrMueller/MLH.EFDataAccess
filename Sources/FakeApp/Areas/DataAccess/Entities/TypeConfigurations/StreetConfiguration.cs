using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities.TypeConfigurations
{
    public class StreetConfiguration : IEntityTypeConfiguration<Street>
    {
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(f => f.StreetName).IsRequired();
            builder.Property(f => f.StreetNumber).IsRequired();

            builder.ToTable("Street", "Core");
        }
    }
}