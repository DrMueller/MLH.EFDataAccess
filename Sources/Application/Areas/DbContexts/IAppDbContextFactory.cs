namespace Mmu.Mlh.EfDataAccess.Areas.DbContexts
{
    public interface IAppDbContextFactory
    {
        IAppDbContext Create();
    }
}