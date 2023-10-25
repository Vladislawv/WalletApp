using WalletApp.Api.Middlewares;

namespace WalletApp.Api;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApiServices(this IServiceCollection services)
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

    public static ILogger<T> CreateConsoleLogger<T>()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        return loggerFactory.CreateLogger<T>();
    }
}