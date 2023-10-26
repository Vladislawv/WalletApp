using WalletApp.Api;
using WalletApp.Infrastructure;
using WalletApp.Application;
using ApiAssemblyConfigurator = WalletApp.Api.AssemblyConfigurator;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureApiServices()
    .ConfigureInfrastructureServices(builder.Configuration)
    .ConfigureApplicationServices();

var logger = ApiAssemblyConfigurator.CreateConsoleLogger<Program>();

var app = builder.Build();

app.UseWebApi();
app.UseHttpsRedirection();
app.UseAuthorization();
app.Services.InitializeDatabase();

logger.LogInformation("WalletApp server has been started.");

app.Run();