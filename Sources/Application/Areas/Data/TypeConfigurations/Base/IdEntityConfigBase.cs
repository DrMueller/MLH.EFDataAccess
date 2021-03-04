using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.Areas.Data.TypeConfigurations.Base
{
    public abstract class IdEntityConfigBase<T> : IEntityTypeConfiguration<T>
        where T : IdEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}