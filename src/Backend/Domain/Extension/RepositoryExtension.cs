using Microsoft.Extensions.Configuration;

namespace Domain.Extension;

public static class RepositoryExtension
{
    public static string GetConnection(this IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Connection");

        return connection;
    }

    public static string GetDb(this IConfiguration configuration)
    {
        var db = configuration.GetConnectionString("DbName");

        return db;
    }
}