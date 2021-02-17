using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.DbContexts.Contexts;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.DbContexts.Factories.Implementation
{
    internal class AppDbContextFactory : IAppDbContextFactory
    {
        private readonly string _databaseName;

        // The Ctor is called per backend setp, which we do for each test
        // Therefore, they way we got for sure a new database per test
        public AppDbContextFactory()
        {
            _databaseName = Guid.NewGuid().ToString();
        }

        public IAppDbContext Create()
        {
            //const string ConnectionString = "Data Source=localhost\\SQLEXPRESS;database=FakeDataAccess;trusted_connection=yes";

            var options = new DbContextOptionsBuilder()

                //.UseSqlServer(ConnectionString)
                .UseInMemoryDatabase(_databaseName)
                .ConfigureWarnings(f => f.Throw())
                .LogTo(
                    str =>
                    {
                        Debug.WriteLine(str);
                    })
                .Options;

            return new AppDbContext(options);
        }
    }
}