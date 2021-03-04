using System.Threading.Tasks;
using Mmu.Mlh.EfDataAccess.Areas.Data.Entities.Base;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts.Contexts;
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

        public ICodeRepository<TEntity> GetCodeRepository<TEntity>() where TEntity : CodeEntity
        {
            var repoType = typeof(ICodeRepository<TEntity>);

            return _repoCache.GetRepository<ICodeRepository<TEntity>>(repoType, _dbContext);
        }

        public IIdRepository<TEntity> GetIdRepository<TEntity>() where TEntity : IdEntity
        {
            var repoType = typeof(IIdRepository<TEntity>);

            return _repoCache.GetRepository<IIdRepository<TEntity>>(repoType, _dbContext);
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