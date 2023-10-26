using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WalletApp.Api.Middlewares;

namespace WalletApp.Api;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureWebApiServices(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers();
        
        services.AddTransient<TransactionIdMiddleware>();
        services.AddTransient<ExceptionHandlerMiddleware>();
        
        return services;
    }

    public static WebApplication UseWebApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseMiddleware<TransactionIdMiddleware>();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        app.MapControllers();

        return app;
    }
}