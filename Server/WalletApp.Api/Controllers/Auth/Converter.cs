using WalletApp.Api.Controllers.Auth.Dto;
using WalletApp.Api.Controllers.Auth.Requests;
using WalletApp.Application.Auth;
using WalletApp.Application.CQRS.Commands;
using WalletApp.Application.CQRS.Queries;

namespace WalletApp.Api.Controllers.Auth;

public static class Converter
{
    public static RegisterCommand ToCommand(this RegisterRequest request)
    {
        return new RegisterCommand(request.UserName, request.Email, request.Password);
    }

    public static GetAuthUserInfoQuery ToQuery(this GetAuthUserInfoRequest request)
    {
        return new GetAuthUserInfoQuery(request.UserNameOrEmail, request.Password);
    }
    
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