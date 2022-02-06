using Cms.ContentArea.Queries;
using Cms.Shared;

namespace Cms.ContentArea
{
    public static class Register
    {
        public static void AddContentArea(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IQueryableRepos<ContentAreaDescription, Guid>, ContentAreaRepos>();
            serviceCollection.AddTransient<IContentAreaService, ContentAreaService>();
            serviceCollection.AddTransient<IPagedQueryProvider<ContentAreaDescription,Guid>,PagedContentArea>(); 
        }
    }
}
