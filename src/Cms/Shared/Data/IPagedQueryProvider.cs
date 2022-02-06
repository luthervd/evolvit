namespace Cms.Shared
{
    public interface IPagedQueryProvider<TEntity, TId> : IEntityCollectionQuery<TEntity, TId> where TEntity : IEntity<TId>
    {
        int PageSize { get; }

        int PageNumber { get;}

        void RegisterPageArgs(int page, int pageSize);  
    }
}
