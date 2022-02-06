using System.Data.Common;

namespace Cms.Shared
{
    public interface IRepos<T, TId> where T : IEntity<TId>
    {
        Task<T> Create(T item);

        Task<T?> Get(TId id);

        Task<T> Update(T item);

        Task<bool> Delete(TId item);

        DbConnection GetConnection();

        void With(DbConnection connection, DbTransaction? transaction = null);

        Task<DbTransaction> WithTransaction();
    }
}
