using Cms.Shared;
using Dapper;

namespace Cms.ContentTemplate
{
    public class ContentTemplateRepos : QueryableRepository<ContentTemplateAggregate, int>
    {
        public ContentTemplateRepos(IConfiguration configuration) : base(configuration)
        {
        }

        public override async Task<ContentTemplateAggregate> Create(ContentTemplateAggregate item)
        {
            var insertIntoContentTemplate = $"INSERT INTO content_template (name) VALUES (@name) RETURNING id;";
            var conn = GetConnection();
            var result = await conn.ExecuteScalarAsync<int>(insertIntoContentTemplate, new { Name = item.Name },Transaction);
            item.Id = result;
            return item;
        }

        public override Task<bool> Delete(int item)
        {
            throw new NotImplementedException();
        }

        public override async Task<ContentTemplateAggregate?> Get(int id)
        {
            var statement = "SELECT * FROM content_template WHERE id = @id";
            var conn = GetConnection();
            var result = await conn.QueryAsync<ContentTemplateAggregate>(statement, new { Id = id });
            return result.FirstOrDefault();
        }

        public override Task<ContentTemplateAggregate> Update(ContentTemplateAggregate item)
        {
            throw new NotImplementedException();
        }
    }
}
