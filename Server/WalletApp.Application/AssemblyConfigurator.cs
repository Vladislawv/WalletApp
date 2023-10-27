using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Shared.InternalMessaging.CQRS;
using WalletApp.Application.DailyPoints;
using WalletApp.Application.Options;
using WalletApp.Application.Users;
using WalletApp.Domain.DailyPointAggregate;
using WalletApp.Domain.UserAggregate;

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

        services.AddTransient<IDailyPointCalculationService, DailyPointCalculationService>();
        services.AddTransient<IUserService, UserService>();
        
        return services;
    }
}