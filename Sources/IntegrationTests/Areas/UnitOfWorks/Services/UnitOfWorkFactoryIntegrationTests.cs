using FluentAssertions;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.DependencyInjection;
using Xunit;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Areas.UnitOfWorks.Services
{
    public class UnitOfWorkFactoryIntegrationTests
    {
        [Fact]
        public void CreatingTwoUnitOfWorks_ReturnsDifferentRepositories()
        {
            // Arrange
            var container = TestContainerFactory.Create();

            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();

            // Act
            var uow1 = uowFactory.Create();
            var uow2 = uowFactory.Create();

            var indRepo1 = uow1.GetRepository<IIndividualRepository>();
            var indRepo2 = uow2.GetRepository<IIndividualRepository>();

            var genericIndRepo1 = uow1.GetGenericRepository<Individual>();
            var genericIndRepo2 = uow2.GetGenericRepository<Individual>();

            var genericAddressRepo1 = uow1.GetGenericRepository<Address>();
            var genericAddressRepo2 = uow2.GetGenericRepository<Address>();

            // Assert
            indRepo1.Should().NotBeSameAs(indRepo2);
            genericIndRepo1.Should().NotBeSameAs(genericIndRepo2);
            genericAddressRepo1.Should().NotBeSameAs(genericAddressRepo2);
        }

        [Fact]
        public void CreatingUnitOfWork_CreatesNewUnitOfWork()
        {
            // Arrange
            var container = TestContainerFactory.Create();

            var sut = container.GetInstance<IUnitOfWorkFactory>();

            // Act
            var uow1 = sut.Create();
            var uow2 = sut.Create();

            // Assert
            uow1.Should().NotBeSameAs(uow2);
        }
    }
}