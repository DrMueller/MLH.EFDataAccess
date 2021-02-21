using System;
using System.Collections.Generic;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Infrastructure.Data
{
    internal static class TestDataFactory
    {
        internal static Address CreateAddress()
        {
            return new Address
            {
                Streets = new List<Street>
                {
                    CreateStreet()
                },
                City = Guid.NewGuid().ToString(),
                Zip = 1234
            };
        }

        internal static Individual CreateIndividual()
        {
            var ind = new Individual
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = "Matthias",
                Birthdate = new DateTime(1986, 12, 29),
                Addresses = new List<Address>
                {
                    CreateAddress()
                }
            };

            return ind;
        }

        internal static Street CreateStreet()
        {
            return new Street
            {
                StreetName = Guid.NewGuid().ToString()
            };
        }
    }
}