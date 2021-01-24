using FluentAssertions;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.DependencyInjection;
using Xunit;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Areas.UnitOfWorks
{
    public class UnitOfWorkFactoryIntegrationTests
    {
        private readonly IUnitOfWorkFactory _sut;

        public UnitOfWorkFactoryIntegrationTests()
        {
            var container = TestContainerFactory.Create();
            _sut = container.GetInstance<IUnitOfWorkFactory>();
        }

        [Fact]
        public void CreatingTwoUnitOfWorks_ReturnsDifferentRepositories()
        {
            // Act
            var uow1 = _sut.Create();
            var uow2 = _sut.Create();

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
            // Act
            var uow1 = _sut.Create();
            var uow2 = _sut.Create();

            // Assert
            uow1.Should().NotBeSameAs(uow2);
        }
    }
}