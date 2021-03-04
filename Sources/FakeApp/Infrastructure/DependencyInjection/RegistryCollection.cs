using JetBrains.Annotations;
using Lamar;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts.Factories;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.DbContexts.Factories.Implementation;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Infrastructure.DependencyInjection
{
    [UsedImplicitly]
    public class RegistryCollection : ServiceRegistry
    {
        public RegistryCollection()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<RegistryCollection>();
                    scanner.AddAllTypesOf(typeof(IIdRepository<>)); // Careful: We can't use just IRepository, I guess because of the generic clause gaurd
                    scanner.AddAllTypesOf(typeof(ICodeRepository<>));
                    scanner.WithDefaultConventions();
                });

            For<IAppDbContextFactory>().Use<AppDbContextFactory>().Singleton();
        }
    }
}