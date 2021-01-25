using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.DbContexts.Contexts;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.DbContexts.Factories.Implementation
{
    internal class AppDbContextFactory : IAppDbContextFactory
    {
        public IAppDbContext Create()
        {
            //const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;database=FakeDataAccess;trusted_connection=yes";

            var options = new DbContextOptionsBuilder()

                //.UseSqlServer(ConnectionString)
                .UseInMemoryDatabase("Test")
                .Options;

            return new AppDbContext(options);
        }
    }
}