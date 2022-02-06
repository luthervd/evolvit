using Cms.Shared;
using System.Data.Common;

namespace Cms.ContentTemplate.Queries
{
    public interface IContentFieldFKQuery
    {
        Task<ICollection<ContentField>> GetFields(DbConnection connection, IEnumerable<int> parentIds); 
    }
}
