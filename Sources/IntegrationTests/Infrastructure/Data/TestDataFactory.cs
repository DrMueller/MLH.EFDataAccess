using System;
using System.Collections.Generic;
using System.Text;
using Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities;

namespace Mmu.Mlh.EfDataAccess.IntegrationTests.Areas.Repositories
{
    internal static class TestDataFactory
    {
        internal static Individual CreateIndividual()
        {
            var ind = new Individual
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = "Matthias",
                Birthdate = new DateTime(1986, 12, 29),
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Streets = new List<Street>
                        {
                            new Street
                            {
                                StreetName = Guid.NewGuid().ToString()
                            }
                        },
                        City = "City",
                        Zip = 1234
                    }
                }
            };

            return ind;
        }
    }
}
