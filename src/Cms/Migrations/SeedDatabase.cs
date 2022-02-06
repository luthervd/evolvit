using FluentMigrator.Runner;

namespace Cms.Migrations
{
    public static class SeedDatabase
    {

        public static void RunMigrations()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddEnvironmentVariables();
            var config = configBuilder.Build();
            var configSection = config.GetSection("EVOLVIT");
            var connString = $"Server={configSection["host"]};Port=5432;Database=evolvit;User Id={configSection["User"]};Password={configSection["Password"]};";
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb.AddPostgres()
                                        .WithGlobalConnectionString(connString)
                                        .ScanIn(typeof(Program).Assembly).For.Migrations())
                                        .AddLogging(lb => lb.AddFluentMigratorConsole());
            using var provider = serviceCollection.BuildServiceProvider();
            var runner = provider.GetService<IMigrationRunner>();
            runner?.MigrateUp();

        }
    }
}
