using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.Repositories.Base;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories.Implementation
{
    public class CountryRepository : CodeRepositoryBase<Country>, ICountryRepository
    {
        public async Task<IReadOnlyCollection<Country>> LoadFirst10Async()
        {
            return await Query.Take(10).ToListAsync();
        }
    }
}