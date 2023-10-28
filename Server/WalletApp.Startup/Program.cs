using WalletApp.Api;
using WalletApp.Infrastructure;
using WalletApp.Application;
using StartupConfigurator = WalletApp.Startup.AssemblyConfigurator;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureWebApiServices()
    .ConfigureApplicationServices(builder.Configuration)
    .ConfigureInfrastructureServices(builder.Configuration);

var logger = StartupConfigurator.CreateConsoleLogger<Program>();

var app = builder.Build();

app.UseWebApi();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Services.InitializeDatabase();

logger.LogInformation("WalletApp server has been started.");

app.Run();