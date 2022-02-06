using Cms.Shared;
using Dapper;

namespace Cms.ContentTemplate
{
    public class ContentFieldRepos : QueryableRepository<ContentField, int>
    {
        public ContentFieldRepos(IConfiguration configuration) : base(configuration)
        {
        }

        public override async Task<ContentField> Create(ContentField item)
        {
            var conn = GetConnection();
            var insert = "INSERT Into content_field (name, type, template_id) VALUES (@name, @type , @contentTemplateId) RETURNING id";
            var result = await  conn.ExecuteScalarAsync<int>(insert, item, Transaction);
            item.Id = result;
            return item;
        }

        public override Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<ContentField> Get(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<ContentField> Update(ContentField item)
        {
            throw new NotImplementedException();
        }
    }
}
