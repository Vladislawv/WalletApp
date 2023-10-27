namespace WalletApp.Api.Controllers.Auth.Requests;

public record RegisterRequest(string UserName, string Email, string Password);