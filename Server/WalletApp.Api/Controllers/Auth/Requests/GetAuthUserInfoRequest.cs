namespace WalletApp.Api.Controllers.Auth.Requests;

public record GetAuthUserInfoRequest(string UserNameOrEmail, string Password);