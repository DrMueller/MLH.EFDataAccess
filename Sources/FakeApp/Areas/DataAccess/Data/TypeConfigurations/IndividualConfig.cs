using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mmu.Mlh.EfDataAccess.Areas.Data.TypeConfigurations.Base;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.TypeConfigurations
{
    public class IndividualConfig : IdEntityConfigBase<Individual>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Individual> builder)
        {
            builder.Property(f => f.Birthdate).IsRequired();
            builder.Property(f => f.FirstName).IsRequired();
            builder.Property(f => f.LastName).IsRequired();
            builder.HasMany(f => f.Addresses).WithOne(f => f.Individual).IsRequired();

            builder.ToTable("Individual", "Core");
        }
    }
}