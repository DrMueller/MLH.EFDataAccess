
using Lamar;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.DependencyInjection
{
    internal static class TestContainerFactory
    {
        internal static IContainer Create()
        {
            return new Container(
                cfg =>
                {
                    cfg.Scan(
                        scanner =>
                        {
                            scanner.AssembliesFromApplicationBaseDirectory();
                            scanner.LookForRegistries();
                        });
                });
        }
    }
}