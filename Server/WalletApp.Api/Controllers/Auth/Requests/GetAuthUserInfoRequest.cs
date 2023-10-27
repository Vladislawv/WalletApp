namespace WalletApp.Api.Controllers.Auth.Requests;

public record GetAuthUserInfoRequest(string UserNameOrEmai, string Password);