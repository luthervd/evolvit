namespace Cms.Shared
{
    public interface IQueryableRepos<T, TId> : IRepos<T, TId> where T : IEntity<TId>
    {
        Task<IQueryResult<IEntity<TId>>> QueryforSingle(IEntityQuery<T,TId> queryProvider);

        Task<IQueryResult<ICollection<T>>> QueryforMany(IEntityCollectionQuery<T,TId> queryProvider);
    }
}
