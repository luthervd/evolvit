using Cms.Shared;
using Npgsql;
using Dapper;
namespace Cms.ContentArea
{
    public class ContentAreaRepos : PostgreSQLQueryableRepository<ContentAreaDescription, Guid>
    {

        public ContentAreaRepos(IConfiguration configuration) : base(configuration) { }

        public override async Task<ContentAreaDescription> Create(ContentAreaDescription item)
        {
            var statement = @"INSERT INTO content_area_description (id, name, description) VALUES (@Id,@Name,@Description)";
            using(var conn  = GetConnection())
            {
               var result = await conn.ExecuteAsync(statement, item);
               if(result != 1)
               {
                    throw new DbInsertException($"Error inserting 1 record for content area result count {result}");
               }
                return item;
            }
        }

        public override Task<bool> Delete(Guid item)
        {
            using (var conn = GetConnection())
            {

            }
            throw new NotImplementedException();
        }

        public override async Task<ContentAreaDescription> Get(Guid id)
        {
            var selectStatement = "SELECT * FROM ContentArea WHERE Id = @Id";
            using (var conn = GetConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<ContentAreaDescription>(selectStatement, id);
                return result;
            }
        }

        public override Task<ContentAreaDescription> Update(ContentAreaDescription item)
        {
            using (var conn = GetConnection())
            {

            }
            throw new NotImplementedException();
        }
    }
}
