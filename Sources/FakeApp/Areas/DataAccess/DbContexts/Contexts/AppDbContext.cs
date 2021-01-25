using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.DbContexts.Contexts
{
    public sealed class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.ConfigureWarnings(warnings => warnings.Throw());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}