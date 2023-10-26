using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Shared.InternalMessaging.CQRS;
using WalletApp.Application.Options;

namespace WalletApp.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCQRS(typeof(AssemblyConfigurator).Assembly);

        services.Configure<AuthOptions>(options =>
        {
            configuration.GetSection(AuthOptions.Section).Bind(options);
        });
        
        services.Configure<IdentityOptions>(options =>
            configuration.GetSection(IdentityOptions.Section).Bind(options));
        
        return services;
    }
}