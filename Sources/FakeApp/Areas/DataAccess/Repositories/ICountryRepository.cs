using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories
{
    public interface ICountryRepository : ICodeRepository<Country>
    {
        Task<IReadOnlyCollection<Country>> LoadFirst10Async();
    }
}