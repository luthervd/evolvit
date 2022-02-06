using Cms.ContentTemplate.Queries;
using Cms.Shared;
using Dapper;
using System.Data.Common;

namespace Cms.ContentTemplate
{
    public class ContentTemplateManager : IContentTemplateManager
    {
        private IQueryableRepos<ContentTemplateAggregate, int> _contentTemplateRepos;
        private IPagedQueryProvider<ContentTemplateAggregate, int> _queryProvider;
        private IContentFieldFKQuery _contentFieldFKQuery;
        private ISaveTemplateUOW _saveUOW;

        public ContentTemplateManager(IQueryableRepos<ContentTemplateAggregate, int> contentTemplateRepos,
            IPagedQueryProvider<ContentTemplateAggregate, int> queryProvider,
            IContentFieldFKQuery contentFieldFKQuery,
            ISaveTemplateUOW saveUOW)
        {
            _contentTemplateRepos = contentTemplateRepos;
            _queryProvider = queryProvider;
            _saveUOW = saveUOW;
            _contentFieldFKQuery = contentFieldFKQuery;
        }

        public async Task<ContentTemplateAggregate> CreateTemplate(ContentTemplateAggregate template)
        {
            return await _saveUOW.DoWork(template);
        }

        public async Task<ContentTemplateAggregate> Load(int id)
        {
            var conn = _contentTemplateRepos.GetConnection();
            var contentTemplate = await _contentTemplateRepos.Get(id);
            contentTemplate.ContentFields = await GetContentFields(conn, id);
            return contentTemplate;
        }

        public async Task<PagedResult<ContentTemplateAggregate>> LoadPaged(int page, int pageSize)
        {

            _queryProvider.RegisterPageArgs(page, pageSize);
            var queryResult = await _contentTemplateRepos.QueryforMany(_queryProvider);
            return new PagedResult<ContentTemplateAggregate>
            {
                PageSize = pageSize,
                PageNumber = page,
                Result = queryResult.Result
            };

        }

        private async Task<ICollection<ContentField>> GetContentFields(DbConnection conn, int id)
        {
            return await _contentFieldFKQuery.GetFields(conn, new[] { id });
        }
    }
}
