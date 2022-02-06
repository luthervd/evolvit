namespace Cms.Shared
{
    public class CollectionQueryResult<TEntity, TId> : IQueryResult<ICollection<TEntity>> where TEntity : IEntity<TId>
    {
        public CollectionQueryResult(ICollection<TEntity> result, bool success = true)
        {
            Result = result;
            Success = success;
        }
        public ICollection<TEntity> Result {get; private set;}

        public bool Success { get; private set;}        
    }
}
