namespace WalletApp.Api;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApiServices(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers();
        
        return services;
    }

    public static WebApplication UseWebApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.MapControllers();

        return app;
    }

    public static ILogger<T> CreateConsoleLogger<T>()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        return loggerFactory.CreateLogger<T>();
    }
}