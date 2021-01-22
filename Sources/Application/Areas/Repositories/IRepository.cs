using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository
        where T : EntityBase
    {
        Task DeleteAsync(long id);

        Task InsertAsync(T[] entities);

        Task<IReadOnlyCollection<T>> LoadAllAsync();

        Task UpsertAsync(T entity);
    }
}