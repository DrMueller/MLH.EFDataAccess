using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Lamar;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.Querying;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.Data;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.DependencyInjection;
using Xunit;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Areas.Querying
{
    public class QueryServiceIntegrationTests
    {
        private readonly IContainer _container;
        private readonly IQueryService _sut;

        public QueryServiceIntegrationTests()
        {
            _container = TestContainerFactory.Create();
            _sut = _container.GetInstance<IQueryService>();
        }

        [Fact]
        public async Task QueryingSingleIndividual_IndividualExisting_QueriesIndividual()
        {
            // Arrange
            var uowFactory = _container.GetInstance<IUnitOfWorkFactory>();
            var uow = uowFactory.Create();
            var indRepo = uow.GetGenericRepository<Individual>();
            var individual = TestDataFactory.CreateIndividual();

            await indRepo.UpsertAsync(individual);
            await uow.SaveAsync();

            // Act
            var actualResult = await _sut.QuerySingleAsync<Individual>(qry => qry.SingleAsync());

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Id.Should().Be(individual.Id);
        }

        [Fact]
        public async Task QueryingSingleIndividual_WithIncludes_QueryIncludes()
        {
            // Arrange
            var uowFactory = _container.GetInstance<IUnitOfWorkFactory>();
            var uow = uowFactory.Create();
            var indRepo = uow.GetGenericRepository<Individual>();
            var individual = TestDataFactory.CreateIndividual();

            await indRepo.UpsertAsync(individual);
            await uow.SaveAsync();

            // Act
            var actualResult = await _sut.QuerySingleAsync<Individual>(
                qry =>
                    qry.Include(f => f.Addresses)
                        .ThenInclude(f => f.Streets)
                        .SingleAsync());

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Addresses.Should().HaveCount(individual.Addresses.Count);
            actualResult.Addresses.SelectMany(f => f.Streets).Count().Should().Be(individual.Addresses.SelectMany(f => f.Streets).Count());
        }
    }
}