using Lamar;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.Areas.Repositories.Implementation;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services.Implementation;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services.Servants;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services.Servants.Implementation;

namespace Mmu.Mlh.EfDataAccess.Infrastructure.DependencyInjection
{
    public class RegistryCollection : ServiceRegistry
    {
        public RegistryCollection()
        {
            For(typeof(IRepository<>)).Use(typeof(CoreRepository<>)).Transient();
            For<UnitOfWork>().Use<UnitOfWork>().Transient();
            For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>().Singleton();
            For<IRepositoryCache>().Use<RepositoryCache>().Transient();
        }
    }
}