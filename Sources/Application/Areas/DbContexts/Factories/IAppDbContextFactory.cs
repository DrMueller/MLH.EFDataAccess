using Mmu.Mlh.EfDataAccess.Areas.DbContexts.Contexts;

namespace Mmu.Mlh.EfDataAccess.Areas.DbContexts.Factories
{
    public interface IAppDbContextFactory
    {
        IAppDbContext Create();
    }
}