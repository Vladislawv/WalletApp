using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletApp.Application.Contracts;
using WalletApp.Application.Options;
using WalletApp.DAL.Models.Users;
using WalletApp.Infrastructure.Auth;
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
        
        services.AddIdentityCore<User>(options =>
        {
            var identityOptions = configuration.GetSection(IdentityOptions.Section).Get<IdentityOptions>();
            options.User.RequireUniqueEmail = identityOptions.RequireUniqueEmail;
            options.Password.RequireDigit = identityOptions.Password.RequireDigit;
            options.Password.RequiredLength = identityOptions.Password.RequiredLength;
            options.Password.RequireLowercase = identityOptions.Password.RequireLowercase;
            options.Password.RequireNonAlphanumeric = identityOptions.Password.RequireNonAlphanumeric;
            options.Password.RequireUppercase = identityOptions.Password.RequireUppercase;
            options.Password.RequiredUniqueChars = identityOptions.Password.RequiredUniqueChars;
        }).AddEntityFrameworkStores<WalletAppContext>();

        services.AddTransient<IAuthTokenManager, AuthJwtManager>();

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