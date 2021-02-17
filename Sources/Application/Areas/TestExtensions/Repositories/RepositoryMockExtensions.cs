using System;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Entities;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Moq;
using Moq.Language.Flow;

namespace Mmu.Mlh.EfDataAccess.Areas.TestExtensions.Repositories
{
    public static class UnitOfWorkMockExtensions
    {
        public static ISetup<IRepository<TEntity>, Task<TRes>> SetupLoading<TEntity, TRes>(this Mock<IRepository<TEntity>> repoMock)
            where TEntity : EntityBase
        {
            return repoMock.Setup(f => f.LoadAsync(It.IsAny<Func<IQueryable<TEntity>, Task<TRes>>>()));
        }
    }
}