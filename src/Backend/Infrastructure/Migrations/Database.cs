using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Migrations;

public static class Database
{
    public static void CreateDb(string dbConnection, string dbName)
    {
        using var myConnection = new SqlConnection(dbConnection);

        var param = new DynamicParameters();
        param.Add("name",dbName);

        var registers = myConnection
            .Query("SELECT * FROM sys.databases WHERE name = @name", param);

        if (!registers.Any())
            myConnection.Execute($"CREATE DATABASE {dbName}");
    }
}