using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WalletApp.Api.Middlewares;

namespace WalletApp.Api;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureWebApiServices(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "WalletApp API", Version = "v1" });
            })
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
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WalletApp API v1");
            });
        }
        
        app.UseMiddleware<TransactionIdMiddleware>();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        app.MapControllers();

        return app;
    }
}