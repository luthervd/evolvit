namespace Cms.Shared
{
    public static class Register
    {
        public static void AddShared(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IQueryParamProvider, QueryParamProvider>();
        }
    }
}
