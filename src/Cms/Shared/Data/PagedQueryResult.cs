namespace Cms.Shared
{
    public record PagedQueryResult<TEntity,TId> : IQueryResult<ICollection<TEntity>> where TEntity : IEntity<TId>
    {
        public PagedQueryResult(ICollection<TEntity>? result, bool success, int pageSize, int pageNumber)
        {
            Result = result;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Success = success;
        }

        public ICollection<TEntity>? Result { get;}
        
        public bool Success { get ; }

        public int PageSize { get;  }

        public int PageNumber { get; }
        
    }
}
