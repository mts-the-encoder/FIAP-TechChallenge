using Domain.Extension;
using Domain.Repositories;
using FluentMigrator.Runner;
using Infrastructure.RepositoryAccess;
using Infrastructure.RepositoryAccess.Repository;
using Infrastructure.RepositoryAccess.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Management.Smo;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        AddFluentMigrator(services, configuration);

        AddRepositories(services);
        AddUnitOfWork(services);
        AddContext(services, configuration);
    }

    private static void AddContext(IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnection();

        services.AddDbContext<AppDbContext>(dbOpt =>
        {
            dbOpt.UseSqlServer(connection);
        });
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>()
            .AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }

    private static void AddFluentMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore().ConfigureRunner(x => x.AddSqlServer()
            .WithGlobalConnectionString(configuration.GetFullConnection()).ScanIn(Assembly.Load("Infrastructure"))
            .For.All());
    }
}