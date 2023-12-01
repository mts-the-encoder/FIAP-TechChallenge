using Domain.Extension;
using Infrastructure;
using Infrastructure.Migrations;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Diagnostics;
using static Serilog.Log;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json").Build();

Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddRepository(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

UpdateDb();

app.Run();

void UpdateDb()
{
    var connection = builder.Configuration.GetConnection();
    var db = builder.Configuration.GetDb();
    Database.CreateDb(connection, db);

    app.MigrateDb();
}