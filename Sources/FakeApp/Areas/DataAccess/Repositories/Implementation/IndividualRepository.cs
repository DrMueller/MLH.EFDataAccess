﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.Repositories.Base;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories.Implementation
{
    public class IndividualRepository : IdRepositoryBase<Individual>, IIndividualRepository
    {
        public async Task<IReadOnlyCollection<Individual>> LoadFirst10Async()
        {
            return await Query.Take(10).ToListAsync();
        }
    }
}