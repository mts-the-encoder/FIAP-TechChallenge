using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Domain.Extension;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        AddFluentMigrator(services, configuration);
    }

    public static void AddFluentMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore().ConfigureRunner(x => x.AddSqlServer()
            .WithGlobalConnectionString(configuration.GetFullConnection()).ScanIn(Assembly.Load("Infrastructure"))
            .For.All());
    }
}