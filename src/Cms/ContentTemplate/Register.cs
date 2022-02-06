using Cms.Shared;
using Cms.ContentTemplate.Queries;

namespace Cms.ContentTemplate
{
    public static class Register
    {
        public static void AddContentTemplate(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IQueryableRepos<ContentTemplateAggregate, int>, ContentTemplateRepos>();
            serviceCollection.AddTransient<IContentTemplateManager,ContentTemplateManager>();
            serviceCollection.AddTransient<IQueryableRepos<ContentField, int>, ContentFieldRepos>();
            serviceCollection.AddTransient<ISaveTemplateUOW, SaveTemplateUOW>();
            serviceCollection.AddTransient<IPagedQueryProvider<ContentTemplateAggregate, int>, PagedContentTemplate>();
            serviceCollection.AddTransient<IContentFieldFKQuery, ContentFieldQuery>();
        }
    }
}
