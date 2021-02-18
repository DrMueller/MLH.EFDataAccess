using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Mmu.Mlh.EfDataAccess.Areas.DbContexts
{
    public interface IAppDbContext : IDisposable
    {
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken token = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        ChangeTracker ChangeTracker { get; }
    }
}