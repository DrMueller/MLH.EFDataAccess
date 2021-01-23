using System;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Internals.DbContexts.Contexts;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Internals.DbContexts.Factories.Implementation
{
    internal class AppDbContextFactory : IAppDbContextFactory
    {
        public IAppDbContext Create()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }
    }
}
