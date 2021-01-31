using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.Data;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.DependencyInjection;
using Xunit;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Areas.Repositories
{
    // Fun fact: Using the same dbset caches the includes kindahow, sow ecreate new ones
    public class RepositoryBaseIntegrationTests
    {
        private readonly IUnitOfWorkFactory _uowFactory;

        public RepositoryBaseIntegrationTests()
        {
            var container = TestContainerFactory.Create();
            _uowFactory = container.GetInstance<IUnitOfWorkFactory>();
        }

        [Fact]
        public async Task Loading_WithoutIncludes_DoesNotLoadIncludes()
        {
            // Arrange
            var individual = TestDataFactory.CreateIndividual();

            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();

                await indRepo.UpsertAsync(individual);
                await uow.SaveAsync();
            }

            // Act
            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetRepository<IRepository<Individual>>();

                var actualInds = await indRepo.LoadAsync(
                    qry =>
                        qry.Where(f => f.FirstName == individual.FirstName));

                // Assert
                actualInds.Single().Addresses.Should().BeNull();
            }
        }

        [Fact]
        public async Task Loading_WithSelectingDto_ReturnsDto()
        {
            // Arrange
            var individual = TestDataFactory.CreateIndividual();

            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();

                await indRepo.UpsertAsync(individual);
                await uow.SaveAsync();
            }

            // Act
            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();

                var actualDtos = await indRepo.LoadAsync(
                    qry =>
                        qry.Where(f => f.FirstName == individual.FirstName)
                            .Select(
                                f => new IndividualDto
                                {
                                    FirstName = f.FirstName,
                                    LastName = f.LastName,
                                    IndividualId = f.Id
                                }));

                // Assert
                var actualDto = actualDtos.Single();
                actualDto.FirstName.Should().Be(individual.FirstName);
                actualDto.LastName.Should().Be(individual.LastName);
                actualDto.IndividualId.Should().Be(individual.Id);
            }
        }

        [Fact]
        public async Task Loading_WithSelectingId_ReturnsId()
        {
            // Arrange
            var individual = TestDataFactory.CreateIndividual();

            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();

                await indRepo.UpsertAsync(individual);
                await uow.SaveAsync();
            }

            // Act
            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();

                var actualIndIds = await indRepo.LoadAsync(
                    qry =>
                        qry.Where(f => f.FirstName == individual.FirstName)
                            .Select(f => f.Id));

                // Assert
                actualIndIds.Single().Should().Be(individual.Id);
            }
        }

        [Fact]
        public async Task Loading_WithWhere_LoadsWhere()
        {
            // Arrange
            var individual1 = TestDataFactory.CreateIndividual();
            var individual2 = TestDataFactory.CreateIndividual();

            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetRepository<IIndividualRepository>();
                await indRepo.UpsertAsync(individual1);
                await indRepo.UpsertAsync(individual2);
                await uow.SaveAsync();
            }

            // Act
            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetRepository<IIndividualRepository>();

                var actualInds = await indRepo.LoadAsync(
                    qry =>
                        qry.Where(f => f.FirstName == individual1.FirstName));

                // Assert
                actualInds.Should().NotBeNull();
                actualInds.Count.Should().Be(1);
            }
        }

        [Fact]
        public async Task Loading_WitIncludes_LoadsIncludes()
        {
            // Arrange
            var individual = TestDataFactory.CreateIndividual();

            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();
                await indRepo.UpsertAsync(individual);
                await uow.SaveAsync();
            }

            // Act
            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();

                var actualInds = await indRepo.LoadAsync(
                    qry =>
                        qry.Where(f => f.FirstName == individual.FirstName)
                            .Include(f => f.Addresses)
                            .ThenInclude(f => f.Streets));

                // Assert
                var actualind = actualInds.Single();
                actualind.Addresses.Should().NotBeNull();
                actualind.Addresses.Count.Should().Be(1);
                actualind.Addresses.Single().Streets.Should().NotBeNull();
                actualind.Addresses.Single().Streets.Count.Should().Be(1);

                var expectedStreetName = actualind.Addresses.Single().Streets.Single().StreetName;
                actualind.Addresses.Single().Streets.Single().StreetName.Should().Be(expectedStreetName);
            }
        }

        [Fact]
        public async Task LoadingSingle_LoadSingle()
        {
            // Arrange
            var individual1 = TestDataFactory.CreateIndividual();
            var individual2 = TestDataFactory.CreateIndividual();

            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetRepository<IIndividualRepository>();
                await indRepo.UpsertAsync(individual1);
                await indRepo.UpsertAsync(individual2);
                await uow.SaveAsync();
            }

            // Act
            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetRepository<IIndividualRepository>();

                var actualInd = await indRepo.LoadAsync(
                    qry =>
                        qry.SingleAsync(f => f.FirstName == individual1.FirstName));

                // Assert
                actualInd.Should().NotBeNull();
                actualInd.Id.Should().Be(individual1.Id);
            }
        }

        [Fact]
        public async Task Upserting_ExistingEntity_UpdatesEntity()
        {
            // Arrange
            var individual = TestDataFactory.CreateIndividual();
            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();
                await indRepo.UpsertAsync(individual);
                await uow.SaveAsync();
            }

            // Act
            var newLastName = Guid.NewGuid().ToString();
            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();

                var actualInd = await indRepo.LoadAsync(
                    qry =>
                        qry.SingleAsync(f => f.Id == individual.Id));

                actualInd.LastName = newLastName;

                await indRepo.UpsertAsync(actualInd);
                await uow.SaveAsync();
            }

            // Assert
            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();

                var actualInd = await indRepo.LoadAsync(
                    qry =>
                        qry.SingleAsync(f => f.Id == individual.Id));

                actualInd.Should().NotBeNull();
                actualInd.LastName.Should().Be(newLastName);
            }
        }

        [Fact]
        public async Task Upserting_NewEntity_InsertsEntity()
        {
            // Arrange
            var individual = TestDataFactory.CreateIndividual();

            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();
                await indRepo.UpsertAsync(individual);
                await uow.SaveAsync();
            }

            // Act
            using (var uow = _uowFactory.Create())
            {
                var indRepo = uow.GetGenericRepository<Individual>();

                var actualInd = await indRepo.LoadAsync(
                    qry =>
                        qry.SingleAsync(f => f.Id == individual.Id));

                // Assert
                actualInd.Should().NotBeNull();
            }
        }
    }
}