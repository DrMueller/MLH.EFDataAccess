using Lamar;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;

namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services.Implementation
{
    internal class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IContainer _container;
        private readonly IDbContextFactory _dbContextFactory;

        public UnitOfWorkFactory(
            IContainer container,
            IDbContextFactory dbContextFactory)
        {
            _container = container;
            _dbContextFactory = dbContextFactory;
        }

        public IUnitOfWork Create()
        {
            var dbContext = _dbContextFactory.Create();
            var unitOfWork = _container.GetInstance<UnitOfWork>();
            unitOfWork.Initialize(dbContext);

            return unitOfWork;
        }
    }
}