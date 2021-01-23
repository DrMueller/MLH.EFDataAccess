using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories
{
    public interface IIndividualRepository : IRepository<Individual>
    {
        Task<IReadOnlyCollection<Individual>> LoadFirst10Async();
    }
}