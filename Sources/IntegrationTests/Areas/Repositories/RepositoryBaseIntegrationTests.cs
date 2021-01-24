using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.DependencyInjection;
using Xunit;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Areas.Repositories
{
    public class RepositoryBaseIntegrationTests
    {
        private readonly IRepository<Individual> _sut;
        private readonly IUnitOfWork _uow;

        public RepositoryBaseIntegrationTests()
        {
            var container = TestContainerFactory.Create();
            var uowFactory = container.GetInstance<IUnitOfWorkFactory>();
            _uow = uowFactory.Create();
            _sut = _uow.GetGenericRepository<Individual>();
        }

        [Fact]
        public async Task Loading_WitIncludes_LoadsIncludes()
        {
            // Arrange
            var streetName = Guid.NewGuid().ToString();
            var ind = new Individual
            {
                FirstName = Guid.NewGuid().ToString(),
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Streets = new List<Street>
                        {
                            new Street
                            {
                                StreetName = streetName
                            }
                        }
                    }
                }
            };

            await _sut.UpsertAsync(ind);
            await _uow.SaveAsync();

            // Act
            var actualInds = await _sut.LoadAsync(
                qry =>
                    qry.Where(f => f.FirstName == ind.FirstName)
                        .Include(f => f.Addresses)
                        .ThenInclude(f => f.Streets));

            // Assert
            actualInds.Should().NotBeNull();
            actualInds.Count.Should().Be(1);
            var actualind = actualInds.Single();
            actualind.Addresses.Should().NotBeNull();
            actualind.Addresses.Count.Should().Be(1);
            actualind.Addresses.Single().Streets.Should().NotBeNull();
            actualind.Addresses.Single().Streets.Count.Should().Be(1);
            actualind.Addresses.Single().Streets.Single().StreetName.Should().Be(streetName);
        }

        [Fact]
        public async Task Loading_WitoutIncludes_DoesntLoadIncludes()
        {
            // Arrange
            var streetName = Guid.NewGuid().ToString();
            var ind = new Individual
            {
                FirstName = Guid.NewGuid().ToString(),
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Streets = new List<Street>
                        {
                            new Street
                            {
                                StreetName = streetName
                            }
                        }
                    }
                }
            };

            await _sut.UpsertAsync(ind);
            await _uow.SaveAsync();

            // Act
            var actualInds = await _sut.LoadAsync(
                qry =>
                    qry.Where(f => f.FirstName == ind.FirstName));

            // Assert
            actualInds.Should().NotBeNull();
            actualInds.Count.Should().Be(1);
            var actualind = actualInds.Single();
            actualind.Addresses.Should().BeNull();
        }
    }
}