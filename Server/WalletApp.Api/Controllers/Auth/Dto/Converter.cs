using WalletApp.Application.Auth;

namespace WalletApp.Api.Controllers.Auth.Dto;

public static class Converter
{
    public static AuthUserInfoDto ToDto(this AuthUserInfo authUserInfo)
    {
        return new AuthUserInfoDto
        {
            Token = authUserInfo.Token.Value,
            TokenExpirationDate = authUserInfo.Token.ExpirationDate,
            UserId = authUserInfo.User.Id,
            UserName = authUserInfo.User.UserName
        };
    }
}