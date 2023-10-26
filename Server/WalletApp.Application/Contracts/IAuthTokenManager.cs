using WalletApp.Application.Auth.Dto;
using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.Contracts;

public interface IAuthTokenManager
{
    public AuthTokenDto Generate(User user);
}