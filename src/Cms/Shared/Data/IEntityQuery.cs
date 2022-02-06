using System.Data;

namespace Cms.Shared
{
    public interface IEntityQuery<TEntity, TId> where TEntity : IEntity<TId>
    {
        Task<IQueryResult<IEntity<TId>>> Query(IDbConnection connection, IEnumerable<object>? args = null);
    }
}
