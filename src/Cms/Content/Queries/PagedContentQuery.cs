using Cms.Shared;
using Dapper;
using System.Data;
using System.Linq;
using System.Text.Json;

namespace Cms.Content.Queries
{
    public sealed class PagedContentQuery : IPagedQueryProvider<ContentData, Guid>
    {
        private string _selectQueryStatement = "SELECT * FROM content ORDER BY created LIMIT @take OFFSET @skip";

        public PagedContentQuery()
        {
            PageSize = 10;
            PageNumber = 1;
        }

        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

        public async Task<IQueryResult<ICollection<ContentData>>> Query(IDbConnection connection)
        {
            var skip = (PageNumber - 1) * PageSize;
            var take = PageSize;
            var parameters = new DynamicParameters(new { skip = skip, take = take });
            
            var dbResult = await connection.QueryAsync(_selectQueryStatement, parameters);
            var result = new List<ContentData>();
            foreach(var item in dbResult)
            {
                var content = new ContentData
                {
                    Id = item.id,
                    Name = item.name,
                    Description = item.description,
                    Data = JsonSerializer.Deserialize<List<CmsContentField>>(item.data)
                };
                result.Add(content);
            }
            
            return new PagedQueryResult<ContentData, Guid>(result, true, PageSize, PageNumber);
        }

        public Task<IQueryResult<ICollection<ContentData>>> Query<T>(IDbConnection connection, IEnumerable<T> args)
        {
            throw new NotImplementedException();
        }

        public void RegisterPageArgs(int page, int pageSize)
        {
            PageNumber = page;
            PageSize = pageSize;
        }
    }
}
