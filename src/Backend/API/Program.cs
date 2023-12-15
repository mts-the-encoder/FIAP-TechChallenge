using Api.Filters;
using Application;
using Application.Services.Mapper;
using Domain.Extension;
using Infrastructure;
using Infrastructure.Migrations;
using Infrastructure.RepositoryAccess;
using Serilog;
using static Serilog.Log;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(opt => opt.Filters.Add(typeof(ExceptionsFilter)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration());
}).CreateMapper());

builder.Services.AddScoped<AuthenticatedUserAttribute>();

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
AddSerilog();

app.Run();

void UpdateDb()
{
    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

    bool? databaseInMemory = context?.Database.ProviderName?.Equals("Microsoft.EntityFrameworkCore.InMemory");

    if (!databaseInMemory.HasValue || !databaseInMemory.Value)
    {
        var connection = builder.Configuration.GetConnection();
        var db = builder.Configuration.GetDb();
        Database.CreateDb(connection, db);
        
        app.MigrateDb();
    }
}

void AddSerilog()
{
    var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.Development.json").Build();

    Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(config)
        .CreateLogger();
}

public partial class Program { }