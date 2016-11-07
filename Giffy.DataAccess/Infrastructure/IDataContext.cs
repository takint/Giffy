using System;
using System.Threading;
using System.Threading.Tasks;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Infrastructure
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}
