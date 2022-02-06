using Cms.Shared;
using System.Data;
using Dapper;
using System.Data.Common;

namespace Cms.ContentTemplate.Queries
{
    public class ContentFieldQuery : IContentFieldFKQuery
    {
        public async Task<ICollection<ContentField>> GetFields(DbConnection connection, IEnumerable<int> parentIds)
        {
            if (parentIds == null)
            {
                throw new ArgumentNullException(nameof(parentIds));
            }
            var query = $"SELECT * FROM content_field WHERE template_id IN ({string.Join(",",parentIds)})";
            var result = await connection.QueryAsync<ContentField>(query);
            return result.ToList();
        }
        
    }
}
