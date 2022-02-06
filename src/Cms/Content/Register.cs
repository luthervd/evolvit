using Cms.Content.Queries;
using Cms.Shared;

namespace Cms.Content
{
    public static class Register
    {
        public static void AddContent(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IQueryableRepos <ContentData,Guid>,ContentRepos>();
            serviceCollection.AddTransient<IPagedQueryProvider<ContentData, Guid>, PagedContentQuery>();
            serviceCollection.AddTransient<IContentService, ContentService>();
        }
    }
}
