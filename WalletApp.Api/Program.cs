using WalletApp.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApiServices();

var logger = AssemblyConfigurator.CreateConsoleLogger<Program>();

var app = builder.Build();

app.UseWebApi();
app.UseHttpsRedirection();
app.UseAuthorization();

logger.LogInformation("WalletApp server has been started.");

app.Run();