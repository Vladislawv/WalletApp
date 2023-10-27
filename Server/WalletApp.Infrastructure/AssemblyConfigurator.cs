using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WalletApp.Application.Contracts;
using WalletApp.Application.Options;
using WalletApp.Domain.CardAggregate;
using WalletApp.Domain.UserAggregate;
using WalletApp.Infrastructure.Auth;
using WalletApp.Infrastructure.DataAccess.Database;
using WalletApp.Infrastructure.DataAccess.Repositories;

namespace WalletApp.Infrastructure;

public static class AssemblyConfigurator
{
    private const string WALLET_APP_POSTGRE_SQL_SERVER = "WalletAppPostgreSqlServer";
    
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WalletAppContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(WALLET_APP_POSTGRE_SQL_SERVER)));
        
        services.ConfigureIdentity(configuration);
        services.ConfigureAuth(configuration);

        services.AddTransient<IAuthTokenManager, AuthJwtManager>();
        services.AddTransient<ICardRepository, CardRepository>();

        return services;
    }

    private static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
    {
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

        return services;
    }
    
    private static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenSection = configuration.GetSection(AuthOptions.Section).GetSection("Token");
        
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                
                    ValidIssuer = tokenSection.GetSection("Issuer").Value,
                    ValidAudience = tokenSection.GetSection("Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSection.GetSection("Secret").Value))
                };
            });

        services.AddAuthorization();

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