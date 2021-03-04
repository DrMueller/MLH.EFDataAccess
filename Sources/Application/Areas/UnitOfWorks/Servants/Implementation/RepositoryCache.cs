using System;
using System.Collections.Concurrent;
using System.Linq;
using JetBrains.Annotations;
using Lamar;
using Mmu.Mlh.EfDataAccess.Areas.DbContexts.Contexts;
using Mmu.Mlh.EfDataAccess.Areas.Repositories;
using Mmu.Mlh.EfDataAccess.Areas.Repositories.Base;
using Mmu.Mlh.LanguageExtensions.Areas.Types.FunctionsResults;

namespace Mmu.Mlh.EfDataAccess.Areas.UnitOfWorks.Servants.Implementation
{
    [UsedImplicitly]
    internal class RepositoryCache : IRepositoryCache
    {
        private readonly IContainer _container;
        private readonly ConcurrentDictionary<Type, IRepository> _repos;

        public RepositoryCache(IContainer container)
        {
            _container = container;
            _repos = new ConcurrentDictionary<Type, IRepository>();
        }

        public TRepo GetRepository<TRepo>(Type repositoryType, IAppDbContext dbContext)
            where TRepo : IRepository
        {
            var getRepoResult = TryGettingRepository<TRepo>(repositoryType);

            return getRepoResult.IsSuccess ? getRepoResult.Value : InitializeRepository<TRepo>(dbContext);
        }

        private TRepo InitializeRepository<TRepo>(IAppDbContext dbContext)
            where TRepo : IRepository
        {
            var repository = _container.GetInstance<TRepo>();

            if (!(repository is IRepositoryBase repoBase))
            {
                throw new ArgumentException($"{nameof(TRepo)} does not implement RepositoryBase.");
            }

            repoBase.Initialize(dbContext);
            _repos.AddOrUpdate(repository.GetType(), repository, (type, repo) => repo);

            return repository;
        }

        private FunctionResult<TRepo> TryGettingRepository<TRepo>(Type repositoryType)
            where TRepo : IRepository
        {
            var cachedRepo = _repos.SingleOrDefault(f => repositoryType.IsAssignableFrom(f.Key));
            var castedRepo = (TRepo)cachedRepo.Value;

            return castedRepo == null ? FunctionResult.CreateFailure<TRepo>() : FunctionResult.CreateSuccess(castedRepo);
        }
    }
}