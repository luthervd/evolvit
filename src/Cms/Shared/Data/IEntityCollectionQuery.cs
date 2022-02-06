using System.Data;

namespace Cms.Shared
{
    public interface IEntityCollectionQuery<TEntity, TId> where TEntity : IEntity<TId>
    {
        Task<IQueryResult<ICollection<TEntity>>> Query<T>(IDbConnection connection, IEnumerable<T> args);
    }
}
