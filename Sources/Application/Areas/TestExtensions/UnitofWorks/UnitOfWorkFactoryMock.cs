using Mmu.Mlh.EfDataAccess.Areas.Entities;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks;
using Moq;

namespace Mmu.Mlh.EfDataAccess.Areas.TestExtensions.UnitofWorks
{
    public class UnitOfWorkFactoryMock
    {
        private readonly Mock<IUnitOfWorkFactory> _uowFactoryMock;
        private readonly Mock<IUnitOfWork> _uowMock;

        public UnitOfWorkFactoryMock()
        {
            _uowFactoryMock = new Mock<IUnitOfWorkFactory>();
            _uowMock = new Mock<IUnitOfWork>();
            _uowFactoryMock.Setup(f => f.Create()).Returns(_uowMock.Object);
        }

        public IUnitOfWorkFactory Object => _uowFactoryMock.Object;

        public Mock<IRepository<TEntity>> RegisterGenericRepoMock<TEntity>()
            where TEntity : EntityBase
        {
            var mock = new Mock<IRepository<TEntity>>();
            _uowMock.Setup(f => f.GetGenericRepository<TEntity>()).Returns(mock.Object);

            return mock;
        }

        public Mock<TRepo> RegisterRepoMock<TRepo>()
            where TRepo : class, IRepository
        {
            var mock = new Mock<TRepo>();
            _uowMock.Setup(f => f.GetRepository<TRepo>()).Returns(mock.Object);

            return mock;
        }
    }
}