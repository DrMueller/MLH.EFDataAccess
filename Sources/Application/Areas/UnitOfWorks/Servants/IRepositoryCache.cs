﻿using System;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;

namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Servants
{
    public interface IRepositoryCache
    {
        TRepo GetRepository<TRepo>(Type repositoryType, IAppDbContext dbContext)
            where TRepo : IRepository;
    }
}