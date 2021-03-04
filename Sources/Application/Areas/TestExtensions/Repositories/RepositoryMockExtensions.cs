using System;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Moq;
using Moq.Language.Flow;

namespace Mmu.Mlh.EfDataAccess.Areas.TestExtensions.Repositories
{
    public static class UnitOfWorkMockExtensions
    {
        public static ISetup<IIdRepository<TEntity>, Task<TRes>> SetupLoading<TEntity, TRes>(this Mock<IIdRepository<TEntity>> repoMock)
            where TEntity : IdEntity
        {
            return repoMock.Setup(f => f.LoadAsync(It.IsAny<Func<IQueryable<TEntity>, Task<TRes>>>()));
        }

        public static ISetup<ICodeRepository<TEntity>, Task<TRes>> SetupLoading<TEntity, TRes>(this Mock<ICodeRepository<TEntity>> repoMock)
            where TEntity : CodeEntity
        {
            return repoMock.Setup(f => f.LoadAsync(It.IsAny<Func<IQueryable<TEntity>, Task<TRes>>>()));
        }

        public static ISetup<IIdRepository<TEntity>, Task> SetupUpserting<TEntity>(this Mock<IIdRepository<TEntity>> repoMock)
            where TEntity : IdEntity
        {
            return repoMock.Setup(f => f.UpsertAsync(It.IsAny<TEntity>()));
        }
    }
}