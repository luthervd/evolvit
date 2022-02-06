using Cms.Shared;

namespace Cms.ContentArea
{
    public class ContentAreaService : IContentAreaService
    {
        private IQueryableRepos<ContentAreaDescription, Guid> _repos;
        private IPagedQueryProvider<ContentAreaDescription, Guid> _collectionQuery;

        public ContentAreaService(IQueryableRepos<ContentAreaDescription,Guid> repos, IPagedQueryProvider<ContentAreaDescription, Guid> collectionQuery)
        {
            _repos = repos;
            _collectionQuery = collectionQuery;
        }

        public async Task<ContentAreaDescription> Create(ContentAreaDescription description)
        {
            if (description.Id == Guid.Empty)
            {
                description.Id = Guid.NewGuid();
            }
            var result = await _repos.Create(description);
            return result;
        }

        public async Task<PagedResult<ContentAreaDescription>> GetPaged(int pageIndex, int pageSize)
        {
            if(pageSize >= 1)
            {
                if(pageIndex < 1)
                {
                    pageIndex = 1;
                }
                _collectionQuery.RegisterPageArgs(pageIndex, pageSize);
            }
            var items = await _repos.QueryforMany(_collectionQuery);
            return new PagedResult<ContentAreaDescription>
            {
                Result = items.Result,
                PageNumber = pageIndex,
                PageSize = pageSize
            };
        }
    }
}
