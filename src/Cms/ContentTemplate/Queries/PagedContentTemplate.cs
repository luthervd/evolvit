using Cms.Shared;
using Dapper;
using System.Data;
using System.Linq;

namespace Cms.ContentTemplate.Queries
{
    public class PagedContentTemplate : IPagedQueryProvider<ContentTemplateAggregate, int>
    {
        private readonly IQueryableRepos<ContentTemplateAggregate, int> _contentTemplateRepos;
        private readonly IQueryableRepos<ContentField, int> _contentFieldRepos;
        private readonly IContentFieldFKQuery _contentFieldQuery;

        public PagedContentTemplate(IQueryableRepos<ContentTemplateAggregate, int> contentTemplateRepos, IQueryableRepos<ContentField, int> contentFieldRepos, IContentFieldFKQuery contentFieldQuery)
        {
            _contentFieldQuery = contentFieldQuery;
            _contentTemplateRepos = contentTemplateRepos;   
            _contentFieldRepos = contentFieldRepos;
        }

        public int PageSize { get; private set; } = 10;

        public int PageNumber { get; private set; } = 1;


        public async Task<IQueryResult<ICollection<ContentTemplateAggregate>>> Query<T>(IDbConnection connection, IEnumerable<T> args)
        {
            using var conn = _contentFieldRepos.GetConnection();
            _contentFieldRepos.With(conn);
            var contentTemplateQuery = "SELECT (id,name) FROM ContentTemplate ORDER BY created DESC LIMIT @take OFFSET @skip";
            var results = await conn.QueryAsync<ContentTemplateAggregate>(contentTemplateQuery, new { Take = PageSize, Skip = PageNumber });
            var ids = results.Select(x => x.Id);
            var templateDict = results.ToDictionary(x => x.Id);
            var fields = await _contentFieldQuery.GetFields(conn, ids);
            foreach(var group in fields.GroupBy(x => x.ContentTemplateId))
            {
                templateDict[group.Key].ContentFields = group.ToList();
            }
            return new QueryResult<ICollection<ContentTemplateAggregate>>(templateDict.Values);


        }

        public void RegisterPageArgs(int page, int pageSize)
        {
            PageNumber = page;
            PageSize = pageSize;
        }

      
    }
}
