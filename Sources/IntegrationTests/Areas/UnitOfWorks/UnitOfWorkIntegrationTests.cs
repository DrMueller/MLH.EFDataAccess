using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.Repositories.Implementation;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Repositories.Implementation;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.Data;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.DependencyInjection;
using Xunit;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Areas.UnitOfWorks
{
    public class UnitOfWorkIntegrationTests
    {
        private readonly IUnitOfWork _sut;

        public UnitOfWorkIntegrationTests()
        {
            var container = TestContainerFactory.Create();
            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();
            _sut = uowFactory.Create();
        }

        [Fact]
        public void Requesting_coderepository_with_specific_implementation_returns_implementation()
        {
            // Act
            var actualIndRepo = _sut.GetCodeRepository<Country>();

            // Assert
            actualIndRepo.Should().BeOfType<CountryRepository>();
        }

        [Fact]
        public void Requesting_id_and_normal_repository_returns_same_instance()
        {
            // Act
            var actualIndRepo1 = _sut.GetIdRepository<Individual>();
            var actualIndRepo2 = _sut.GetRepository<IIndividualRepository>();

            // Assert
            actualIndRepo1.Should().BeSameAs(actualIndRepo2);
        }

        [Fact]
        public void Requesting_idrepositories_of_different_entities_returns_different_instances()
        {
            // Act
            var actualStreetRepo = _sut.GetIdRepository<Address>();
            var actualAddressRepo = _sut.GetIdRepository<Individual>();

            // Assert
            actualStreetRepo.Should().NotBeSameAs(actualAddressRepo);
        }

        [Fact]
        public void Requesting_idrepository_multiple_times_returns_same_instance()
        {
            // Act
            var actualIndRepo1 = _sut.GetIdRepository<Address>();
            var actualIndRepo2 = _sut.GetIdRepository<Address>();

            // Assert
            actualIndRepo1.Should().BeSameAs(actualIndRepo2);
        }

        [Fact]
        public void Requesting_idrepository_with_specific_implementation_returns_implementation()
        {
            // Act
            var actualIndRepo = _sut.GetIdRepository<Individual>();

            // Assert
            actualIndRepo.Should().BeOfType<IndividualRepository>();
        }

        [Fact]
        public void Requesting_idrepository_without_specific_implementation_returns_core_repository()
        {
            // Act
            var actualIndRepo = _sut.GetIdRepository<Address>();

            // Assert
            actualIndRepo.Should().BeOfType<IdCoreRepository<Address>>();
        }

        [Fact]
        public void Requesting_normal_and_idrepository_returns_same_instance()
        {
            // Act
            var actualIndRepo1 = _sut.GetRepository<IIndividualRepository>();
            var actualIndRepo2 = _sut.GetIdRepository<Individual>();

            // Assert
            actualIndRepo1.Should().BeSameAs(actualIndRepo2);
        }

        [Fact]
        public void Requesting_repository_multiple_times_returns_same_instance()
        {
            // Act
            var actualIndRepo1 = _sut.GetRepository<IIndividualRepository>();
            var actualIndRepo2 = _sut.GetRepository<IIndividualRepository>();

            // Assert
            actualIndRepo1.Should().BeSameAs(actualIndRepo2);
        }

        [Fact]
        public async Task Saving_SavesAllRepositories()
        {
            // Arrange
            var container = TestContainerFactory.Create();
            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();
            var sut = uowFactory.Create();
            var indRepo = sut.GetRepository<IIndividualRepository>();
            var addressRepo = sut.GetIdRepository<Address>();
            var currencyRepo = sut.GetCodeRepository<Currency>();

            // Act
            var individual = TestDataFactory.CreateIndividual();

            var address = new Address
            {
                City = Guid.NewGuid().ToString(),
                Zip = 1715,
                Individual = TestDataFactory.CreateIndividual()
            };

            var currency = new Currency
            {
                Code = "CHF",
                Description = "Swiss francs"
            };

            await indRepo.UpsertAsync(individual);
            await addressRepo.UpsertAsync(address);
            await currencyRepo.InsertAsync(currency);

            await sut.SaveAsync();

            // Assert
            var uowFactory2 = container.GetInstance<IUnitOfWorkFactory>();
            var sut2 = uowFactory2.Create();
            var indRepo2 = sut2.GetRepository<IIndividualRepository>();
            var addressRepo2 = sut2.GetIdRepository<Address>();
            var currencyRepo2 = sut2.GetCodeRepository<Currency>();

            var actualIndividuals = await indRepo2.LoadAsync(qry => qry.Where(f => f.FirstName == individual.FirstName).ToListAsync());
            var actualAddresses = await addressRepo2.LoadAsync(qry => qry.Where(f => f.City == address.City).ToListAsync());
            var actualCurrencies = await currencyRepo2.LoadAsync(qry => qry.Where(f => f.Code == currency.Code).ToListAsync());

            actualIndividuals.Count.Should().Be(1);
            actualAddresses.Count.Should().Be(1);
            actualCurrencies.Count.Should().Be(1);
        }
    }
}