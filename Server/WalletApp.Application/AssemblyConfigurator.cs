﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WalletApp.Application.DailyPoints;
using WalletApp.Application.Options;
using WalletApp.Application.Transactions;
using WalletApp.Domain.Aggregates.DailyPointAggregate;
using WalletApp.Domain.Aggregates.TransactionAggregate;

namespace WalletApp.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCQRS();
        services.ConfigureOptions(configuration);

        services.AddTransient<IDailyPointCalculationService, DailyPointCalculationService>();
        services.AddTransient<ITransactionGenerator, TransactionGenerator>();

        return services;
    }

    private static IServiceCollection AddCQRS(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(AssemblyConfigurator).Assembly));
        return services;
    }

    private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(options => configuration.GetSection(AuthOptions.Section).Bind(options));
        services.Configure<IdentityOptions>(options => configuration.GetSection(IdentityOptions.Section).Bind(options));
        return services;
    }
}