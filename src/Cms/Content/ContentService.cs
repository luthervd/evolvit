using Cms.Shared;

namespace Cms.Content
{
    public class ContentService : IContentService
    {
        private readonly IQueryableRepos<ContentData,Guid> _repos;
        private readonly IPagedQueryProvider<ContentData,Guid> _queryProvider;

        public ContentService(IQueryableRepos<ContentData, Guid> repos, IPagedQueryProvider<ContentData,Guid> pagedQuery)
        {
            _repos = repos;
            _queryProvider = pagedQuery;
        }

        public async Task<ContentData> Create(ContentData contentData)
        {
            return await _repos.Create(contentData);
        }

        public async Task<ContentData> Get(Guid id)
        {
            return await _repos.Get(id);
        }

        public async Task<ICollection<ContentData>> GetPaged(int pageNumber, int pageSize)
        {
            if(pageSize <= 0)
            {
                pageSize = 10;
                
            }
            if(pageNumber <= 0)
            {
                pageNumber = 1;
            }
            _queryProvider.RegisterPageArgs(pageNumber, pageSize);
            var result = await _repos.QueryforMany(_queryProvider);
            return result.Result;
        }
    }
}
