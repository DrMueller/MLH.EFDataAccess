﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories
{
    public interface IIndividualRepository : IIdRepository<Individual>
    {
        Task<IReadOnlyCollection<Individual>> LoadFirst10Async();
    }
}