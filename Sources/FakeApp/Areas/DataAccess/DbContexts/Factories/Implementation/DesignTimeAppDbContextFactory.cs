using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.DbContexts.Contexts;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.DbContexts.Factories.Implementation
{
    [UsedImplicitly]
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