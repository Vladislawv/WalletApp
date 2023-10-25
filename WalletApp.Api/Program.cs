using WalletApp.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApiServices();

var app = builder.Build();

app.UseWebApi();
app.UseHttpsRedirection();
app.UseAuthorization();

app.Run();