using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.Areas.Entities;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Servants;

namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Implementation
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryCache _repoCache;
        private IAppDbContext _dbContext;

        public UnitOfWork(IRepositoryCache repoCache)
        {
            _repoCache = repoCache;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public IRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : EntityBase
        {
            var genericRepoType = typeof(IRepository<TEntity>);
            return _repoCache.GetRepository<IRepository<TEntity>>(genericRepoType, _dbContext);
        }

        public TRepo GetRepository<TRepo>() where TRepo : IRepository
        {
            var repoType = typeof(TRepo);
            return _repoCache.GetRepository<TRepo>(repoType, _dbContext);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        internal void Initialize(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}