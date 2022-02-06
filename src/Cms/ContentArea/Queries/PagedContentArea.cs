using Cms.Shared;

namespace Cms.ContentArea.Queries
{
    public sealed class PagedContentArea : PagedQueryProvider<ContentAreaDescription, Guid>
    {
        public PagedContentArea(IQueryParamProvider queryProvider) : base(queryProvider)
        {
            SelectQueryStatement = "SELECT * FROM content_area_description ORDER BY created LIMIT @take OFFSET @skip";
        }

        protected override string SelectQueryStatement { get ; set; }
    }
}
