namespace Cms.Shared
{
    public class QueryParamProvider : IQueryParamProvider
    {
        public PageQuery GetDefaultPageQuery()
        {
            return new PageQuery();
        }

    }
}
