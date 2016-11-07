using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Infrastructure
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepository<TEntity> RepositoryBase<TEntity>() where TEntity : class, IObjectState;
    }
}
