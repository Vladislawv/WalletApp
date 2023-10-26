using Microsoft.Extensions.DependencyInjection;
using Shared.InternalMessaging.CQRS;

namespace WalletApp.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddCQRS(typeof(AssemblyConfigurator).Assembly);
        return services;
    }
}