namespace WalletApp.Startup;

public static class AssemblyConfigurator
{
    public static ILogger<T> CreateConsoleLogger<T>()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        return loggerFactory.CreateLogger<T>();
    }
}