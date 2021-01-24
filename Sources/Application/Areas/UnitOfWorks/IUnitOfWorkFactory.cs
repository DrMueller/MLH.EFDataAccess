namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}