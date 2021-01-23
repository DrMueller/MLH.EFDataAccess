using Mmu.Mlh.EfDataAccess.Areas.Entities;

namespace Mmu.Mlh.EfDataAccess.FakeApp.Areas.DataAccess.Entities
{
    public class Street : EntityBase
    {
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
    }
}