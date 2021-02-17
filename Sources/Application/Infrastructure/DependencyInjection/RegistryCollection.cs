using JetBrains.Annotations;
using Lamar;
using Mmu.Mlh.EfDataAccess.Areas.Querying;
using Mmu.Mlh.EfDataAccess.Areas.Querying.Implementation;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.Areas.Repositories.Implementation;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Implementation;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Servants;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Servants.Implementation;

namespace Mmu.Mlh.EfDataAccess.Infrastructure.DependencyInjection
{
    [UsedImplicitly]
    public class RegistryCollection : ServiceRegistry
    {
        public RegistryCollection()
        {
            For(typeof(IRepository<>)).Use(typeof(CoreRepository<>)).Transient();
            For<UnitOfWork>().Use<UnitOfWork>().Transient();
            For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>().Singleton();
            For<IRepositoryCache>().Use<RepositoryCache>().Transient();
            For<IQueryService>().Use<QueryService>().Singleton();
        }
    }
}