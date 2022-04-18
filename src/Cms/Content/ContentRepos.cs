using Cms.Shared;
using Dapper;
using System.Text.Json;
using System.Linq;

namespace Cms.Content
{
    public class ContentRepos : QueryableRepository<ContentData, Guid>
    { 
        public ContentRepos(IConfiguration configuration) : base(configuration)
        {
        }

        public override async Task<ContentData> Create(ContentData item)
        {
            using(var conn  = GetConnection())
            {
                var json = JsonSerializer.Serialize(item.Data);
                var command = "INSERT INTO content (id,name,description,data) VALUES (@id,@name,@description,CAST(@data as json))";
                var result = await conn.ExecuteAsync(command,new { Id = item.Id, Name = item.Name,Description = item.Description, Data = json});
                return item;
            }
        }

        public override Task<bool> Delete(Guid item)
        {
            throw new NotImplementedException();
        }

        public override async Task<ContentData?> Get(Guid id)
        {
            using (var conn = GetConnection())
            {
                var query = $"SELECT * FROM content WHERE id = '{id}'";
                var result = await conn.QueryAsync(query);
                var item = result.FirstOrDefault();
                return item != null ? new ContentData
                {
                    Id = item.id,
                    Name = item.name,
                    Description = item.description,
                    Data = JsonSerializer.Deserialize<List<CmsContentField>>(item.data)
                } :  null;
            }
        }

        public override async Task<ContentData> Update(ContentData item)
        {
            var query = $"UPDATE content SET name = {item.Name},description = {item.Description},data = {item.Data} WEHRE id = '{item.Id}'";
            using var conn = GetConnection();
            var result = await conn.ExecuteAsync(query);
            return item;
        }
    }
}
