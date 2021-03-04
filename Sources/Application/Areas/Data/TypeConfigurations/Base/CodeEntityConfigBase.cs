using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.Areas.Data.TypeConfigurations.Base
{
    public abstract class CodeEntityConfigBase<T> : IEntityTypeConfiguration<T>
        where T : CodeEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(f => f.Code);
            builder.Property(f => f.Code).IsRequired().HasMaxLength(200).ValueGeneratedNever();

            ConfigureEntity(builder);
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}