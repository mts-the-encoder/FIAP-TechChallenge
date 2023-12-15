﻿using Domain.Extension;
using FluentMigrator.Runner;
using Infrastructure.RepositoryAccess;
using Infrastructure.RepositoryAccess.Repository;
using Infrastructure.RepositoryAccess.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Management.Smo;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories.User;

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
        bool.TryParse(configuration.GetSection("Configurations:DatabaseInMemory").Value, out bool databaseInMemory);

        if (!databaseInMemory)
        {
            var connection = configuration.GetConnection();

            services.AddDbContext<AppDbContext>(dbOpt =>
            {
                dbOpt.UseSqlServer(connection);
            });
        }
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>()
            .AddScoped<IUserWriteOnlyRepository, UserRepository>()
            .AddScoped<IUserUpdateOnlyRepository, UserRepository>();
    }

    private static void AddFluentMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        bool.TryParse(configuration.GetSection("Configurations:DatabaseInMemory").Value,out bool databaseInMemory);

        if (!databaseInMemory)
        {
            services.AddFluentMigratorCore().ConfigureRunner(x => x.AddSqlServer()
            .WithGlobalConnectionString(configuration.GetFullConnection()).ScanIn(Assembly.Load("Infrastructure"))
            .For.All());
        }

    }
}