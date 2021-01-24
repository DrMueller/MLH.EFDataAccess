using FluentAssertions;
using Mmu.Mlh.EfDataAccess.Areas.Repositories.Implementation;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories.Implementation;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.DependencyInjection;
using Xunit;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Areas.UnitOfWorks.Services
{
    public class UnitOfWokIntegrationTests
    {
        [Fact]
        public void Requesting_generic_and_normal_repository_returns_same_instance()
        {
            // Arrange
            var container = TestContainerFactory.Create();
            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();
            var sut = uowFactory.Create();

            // Act
            var actualIndRepo1 = sut.GetGenericRepository<Individual>();
            var actualIndRepo2 = sut.GetRepository<IIndividualRepository>();

            // Assert
            actualIndRepo1.Should().BeSameAs(actualIndRepo2);
        }

        [Fact]
        public void Requesting_generic_repositories_of_different_entities_returns_different_instances()
        {
            // Arrange
            var container = TestContainerFactory.Create();
            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();
            var sut = uowFactory.Create();

            // Act
            var actualStreetRepo = sut.GetGenericRepository<Address>();
            var actualAddressRepo = sut.GetGenericRepository<Individual>();

            // Assert
            actualStreetRepo.Should().NotBeSameAs(actualAddressRepo);
        }

        [Fact]
        public void Requesting_generic_Repository_multiple_times_returns_same_instance()
        {
            // Arrange
            var container = TestContainerFactory.Create();
            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();
            var sut = uowFactory.Create();

            // Act
            var actualIndRepo1 = sut.GetGenericRepository<Address>();
            var actualIndRepo2 = sut.GetGenericRepository<Address>();

            // Assert
            actualIndRepo1.Should().BeSameAs(actualIndRepo2);
        }

        [Fact]
        public void Requesting_generic_Repository_with_specific_implementation_returns_implementation()
        {
            // Arrange
            var container = TestContainerFactory.Create();
            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();
            var sut = uowFactory.Create();

            // Act
            var actualIndRepo = sut.GetGenericRepository<Individual>();

            // Assert
            actualIndRepo.Should().BeOfType<IndividualRepository>();
        }

        [Fact]
        public void Requesting_generic_Repository_without_specific_implementation_returns_core_repository()
        {
            // Arrange
            var container = TestContainerFactory.Create();
            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();
            var sut = uowFactory.Create();

            // Act
            var actualIndRepo = sut.GetGenericRepository<Address>();

            // Assert
            actualIndRepo.Should().BeOfType<CoreRepository<Address>>();
        }

        [Fact]
        public void Requesting_normal_and_generic_repository_returns_same_instance()
        {
            // Arrange
            var container = TestContainerFactory.Create();
            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();
            var sut = uowFactory.Create();

            // Act
            var actualIndRepo1 = sut.GetRepository<IIndividualRepository>();
            var actualIndRepo2 = sut.GetGenericRepository<Individual>();

            // Assert
            actualIndRepo1.Should().BeSameAs(actualIndRepo2);
        }

        [Fact]
        public void Requesting_repository_multiple_times_returns_same_instance()
        {
            // Arrange
            var container = TestContainerFactory.Create();
            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();
            var sut = uowFactory.Create();

            // Act
            var actualIndRepo1 = sut.GetRepository<IIndividualRepository>();
            var actualIndRepo2 = sut.GetRepository<IIndividualRepository>();

            // Assert
            actualIndRepo1.Should().BeSameAs(actualIndRepo2);
        }
    }
}