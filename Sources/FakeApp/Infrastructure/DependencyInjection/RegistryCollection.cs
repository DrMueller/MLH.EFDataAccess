using Lamar;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Internals.DbContexts.Factories.Implementation;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Infrastructure.DependencyInjection
{
    public class RegistryCollection : ServiceRegistry
    {
        public RegistryCollection()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<RegistryCollection>();
                    scanner.AddAllTypesOf(typeof(IRepository<>));
                    scanner.WithDefaultConventions();
                });

            For<IAppDbContextFactory>().Use<AppDbContextFactory>().Singleton();
        }
    }
}