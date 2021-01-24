namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}