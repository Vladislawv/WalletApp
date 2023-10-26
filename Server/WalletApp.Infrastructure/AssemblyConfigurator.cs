using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletApp.Infrastructure.DataAccess.Database;

namespace WalletApp.Infrastructure;

public static class AssemblyConfigurator
{
    private const string WALLET_APP_POSTGRE_SQL_SERVER = "WalletAppPostgreSqlServer";
    
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WalletAppContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(WALLET_APP_POSTGRE_SQL_SERVER)));

        return services;
    }

    public static IServiceProvider InitializeDatabase(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var walletAppContext = scope.ServiceProvider.GetRequiredService<WalletAppContext>();
        walletAppContext.Database.Migrate();

        return services;
    }
}