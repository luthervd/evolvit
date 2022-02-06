using System.Data;

namespace Cms.Shared
{
    public interface IEntityCollectionQuery<TEntity, TId> where TEntity : IEntity<TId>
    {
        Task<IQueryResult<ICollection<TEntity>>> Query(IDbConnection connection);
    }
}
