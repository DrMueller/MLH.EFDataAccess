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
            // We don't want to do that here, this should the client do
            // But nice to see how it should work
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<RegistryCollection>();
                    scanner.AddAllTypesOf(typeof(IRepository<>));
                    scanner.WithDefaultConventions();
                });

            For(typeof(IIdRepository<>)).Use(typeof(IdCoreRepository<>)).Transient();
            For(typeof(ICodeRepository<>)).Use(typeof(CodeCoreRepository<>)).Transient();
            For<UnitOfWork>().Use<UnitOfWork>().Transient();
            For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>().Singleton();
            For<IQueryService>().Use<QueryService>().Singleton();
            For<IRepositoryCache>().Use<RepositoryCache>().Transient();
        }
    }
}