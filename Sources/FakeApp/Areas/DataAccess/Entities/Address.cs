using System.Collections.Generic;
using JetBrains.Annotations;
using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities
{
    [UsedImplicitly]
    public class Address : EntityBase
    {
        public string City { get; set; }
        public Individual Individual { get; set; }
        public ICollection<Street> Streets { get; set; }
        public int Zip { get; set; }
    }
}