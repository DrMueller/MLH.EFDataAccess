using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities
{
    [PublicAPI]
    public class Individual : IdEntity
    {
        public ICollection<Address> Addresses { get; set; }
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}