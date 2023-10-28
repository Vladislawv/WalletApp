namespace WalletApp.Api.Controllers.Auth.Dto;

public class AuthUserInfoDto
{
    public string Token { get; set; }
    public DateTimeOffset TokenExpirationDate { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}