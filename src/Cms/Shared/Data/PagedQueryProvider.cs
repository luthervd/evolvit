using Cms.Shared;
using Dapper;
using System.Data;

namespace Cms.Shared
{
    public abstract class PagedQueryProvider<TEntity, TId> : IPagedQueryProvider<TEntity,TId> where TEntity : IEntity<TId>
    {
        protected readonly IQueryParamProvider _queryProvider;
        
        protected abstract string SelectQueryStatement { get; set; }

        public PagedQueryProvider(IQueryParamProvider queryProvider)
        {
            _queryProvider = queryProvider;
            
        }

        public int PageSize { get; protected set; }
        public int PageNumber { get; protected set; }

        public async Task<IQueryResult<ICollection<TEntity>>> Query<T>(IDbConnection connection, IEnumerable<T> args = null)
        {
            SetQuerySizeIfEmpty();
            var skip = (PageNumber -1) * PageSize;
            var take = PageSize;
            var parameters = new DynamicParameters(new {skip = skip, take = take });
            var result = await connection.QueryAsync<TEntity>(SelectQueryStatement, parameters);
            if(result == null)
            {
                result = new List<TEntity>();
            }
            else
            {
                result = result.ToList();
            }
            return new PagedQueryResult<TEntity,TId>(result as ICollection<TEntity>, true, PageSize, PageNumber);
        }

        public void RegisterPageArgs(int page, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = page;
        }

        private void SetQuerySizeIfEmpty()
        {
            var defaultPageSize = _queryProvider.GetDefaultPageQuery();
            if(PageSize <= 0)
            {
                PageSize = defaultPageSize.PageSize;
            }
        }
    }
}
