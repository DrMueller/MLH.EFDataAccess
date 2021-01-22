using Lamar;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services.Implementation;

namespace Mmu.Mlh.EfDataAccess.Infrastructure.DependencyInjection
{
    public class RegistryCollection : ServiceRegistry
    {
        public RegistryCollection()
        {
            For<UnitOfWork>().Use<UnitOfWork>().Transient();
            For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>().Singleton();
        }
    }
}