using System;
using System.Linq;
using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Entities;
using Mmu.Mlh.EfDataAccess.Areas.Querying;
using Moq;
using Moq.Language.Flow;

namespace Mmu.Mlh.EfDataAccess.Areas.TestExtensions.Querying
{
    public static class QueryServiceMockExtensions
    {
        public static ISetup<IQueryService, Task<TResult>> SetupQuerying<TEntity, TResult>(this Mock<IQueryService> queryServiceMock)
            where TEntity : EntityBase
        {
            return queryServiceMock
                .Setup(f => f.QueryAsync(It.IsAny<Func<IQueryable<TEntity>, Task<TResult>>>()));
        }

        public static ISetup<IQueryService, Task<TEntity>> SetupQuerying<TEntity>(this Mock<IQueryService> queryServiceMock)
            where TEntity : EntityBase
        {
            return queryServiceMock
                .Setup(f => f.QuerySingleAsync(It.IsAny<Func<IQueryable<TEntity>, Task<TEntity>>>()));
        }
    }
}