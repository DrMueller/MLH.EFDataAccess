namespace Mmu.Mlh.EfDataAccess.Areas.DbContexts
{
    public interface IDbContextFactory
    {
        IDbContext Create();
    }
}