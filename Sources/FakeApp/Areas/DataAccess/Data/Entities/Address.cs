using System.Collections.Generic;
using JetBrains.Annotations;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Data.Entities
{
    [PublicAPI]
    public class Address : IdEntity
    {
        public string City { get; set; }
        public Individual Individual { get; set; }
        public ICollection<Street> Streets { get; set; }
        public int Zip { get; set; }
    }
}