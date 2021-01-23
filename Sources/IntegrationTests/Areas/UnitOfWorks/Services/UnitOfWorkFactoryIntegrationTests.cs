using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services;
using Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.DependencyInjection;
using Xunit;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Areas.UnitOfWorks.Services
{
    public class UnitOfWorkFactoryIntegrationTests
    {
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
