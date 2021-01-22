using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Lamar;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts;
using Mmu.Mlh.EfDataAccess.Areas.Entities;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.Areas.Repositories.Implementation;

namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Services.Implementation
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ConcurrentDictionary<Type, IRepository> _repos;
        private readonly IContainer _serviceLocator;
        private IDbContext _dbContext;

        public UnitOfWork(IContainer serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _repos = new ConcurrentDictionary<Type, IRepository>();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public IRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : EntityBase
        {
            var genericRepoType = typeof(IRepository<TEntity>);
            return GetRepository<IRepository<TEntity>>(genericRepoType);
        }

        public TRepo GetRepository<TRepo>() where TRepo : IRepository
        {
            var repoType = typeof(TRepo);
            return GetRepository<TRepo>(repoType);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        internal void Initialize(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private TRepo GetRepository<TRepo>(Type repoType)
            where TRepo : IRepository
        {
            if (_repos.ContainsKey(repoType))
            {
                return (TRepo)_repos[repoType];
            }

            var repository = _serviceLocator.GetInstance<TRepo>();

            if (!(repository is RepositoryBase repoBase))
            {
                throw new ArgumentException($"{nameof(TRepo)} does not implement RepositoryBase");
            }

            repoBase.Initialize(_dbContext);
            _repos.AddOrUpdate(repoType, repository, (type, repo) => repo);

            return repository;
        }
    }
}