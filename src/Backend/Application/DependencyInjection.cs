using Application.Services.Cryptography;
using Application.Services.Token;
using Application.Services.UserSigned;
using Application.UseCases.Login.DoLogin;
using Application.UseCases.User.Create;
using Application.UseCases.User.UpdatePassword;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Management.Smo.Wmi;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAdditionalKeyPassword(services, configuration);
        AddTokenJWT(services, configuration);
        AddUserSigned(services);
        AddUseCase(services);
    }

    private static void AddUserSigned(IServiceCollection services)
    {
        services.AddScoped<IUserSigned, UserSigned>();
    }

    private static void AddAdditionalKeyPassword(IServiceCollection services, IConfiguration configuration)
    {
        var value = configuration.GetValue<string>("Configurations:AdditionalKey");
        services.AddScoped(opt => new Encryptor(value));
    }

    private static void AddTokenJWT(IServiceCollection services, IConfiguration configuration)
    {
        var sectionLifeTime = configuration.GetValue<string>("Configurations:LifeTime");
        var sectionToken = configuration.GetValue<string>("Configurations:TokenKey");

        services.AddScoped(opt => new TokenService(int.Parse(sectionLifeTime), sectionToken));
    }

    private static void AddUseCase(IServiceCollection services)
    {
        services.AddScoped<ICreateUseCase, CreateUseCase>()
            .AddScoped<ILoginUseCase, LoginUseCase>()
            .AddScoped<IUpdatePasswordUseCase, UpdatePasswordUseCase>();
    }
}
