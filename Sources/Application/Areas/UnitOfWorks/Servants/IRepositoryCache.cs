using System;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts.Contexts;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;

namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Servants
{
    internal interface IRepositoryCache
    {
        TRepo GetRepository<TRepo>(Type repositoryType, IAppDbContext dbContext)
            where TRepo : IRepository;
    }
}