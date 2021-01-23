using System.Collections.Generic;
using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities
{
    public class Address : EntityBase
    {
        public string City { get; set; }
        public ICollection<Street> Streets { get; set; }
        public int Zip { get; set; }
    }
}