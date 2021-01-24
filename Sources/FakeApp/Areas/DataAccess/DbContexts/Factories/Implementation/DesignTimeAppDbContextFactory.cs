using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Internals.DbContexts.Contexts;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Internals.DbContexts.Factories.Implementation
{
    public class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }
    }
}