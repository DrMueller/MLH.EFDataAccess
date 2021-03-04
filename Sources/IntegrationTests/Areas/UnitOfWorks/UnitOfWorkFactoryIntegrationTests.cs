using FluentAssertions;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;
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

            var idIndRepo1 = uow1.GetIdRepository<Individual>();
            var idIndRepo2 = uow2.GetIdRepository<Individual>();

            var adrRepo1 = uow1.GetIdRepository<Address>();
            var adrRepo2 = uow2.GetIdRepository<Address>();

            var currencyRepo1 = uow1.GetRepository<ICountryRepository>();
            var currencyRepo2 = uow2.GetRepository<ICountryRepository>();

            var countryRepo1 = uow1.GetCodeRepository<Country>();
            var countryRepo2 = uow2.GetCodeRepository<Country>();

            // Assert
            idIndRepo1.Should().NotBeSameAs(idIndRepo2);
            indRepo1.Should().NotBeSameAs(indRepo2);
            adrRepo1.Should().NotBeSameAs(adrRepo2);
            currencyRepo1.Should().NotBeSameAs(currencyRepo2);
            countryRepo1.Should().NotBeSameAs(countryRepo2);
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