using System.Threading.Tasks;
using JetBrains.Annotations;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.Areas.Repositories
{
    [PublicAPI]
    public interface ICodeRepository<TCodeEntity> : IRepository<TCodeEntity>
        where TCodeEntity : CodeEntity
    {
        Task DeleteAsync(string code);

        Task InsertAsync(TCodeEntity entity);
    }
}