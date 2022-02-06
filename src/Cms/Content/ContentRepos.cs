using Cms.Shared;
using Dapper;
using System.Text.Json;

namespace Cms.Content
{
    public class ContentRepos : PostgreSQLQueryableRepository<ContentData, Guid>
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

        public override Task<ContentData> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<ContentData> Update(ContentData item)
        {
            throw new NotImplementedException();
        }
    }
}
