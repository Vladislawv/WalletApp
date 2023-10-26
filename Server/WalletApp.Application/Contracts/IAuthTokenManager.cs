using WalletApp.Application.Auth;
using WalletApp.DAL.Models.Users;

namespace WalletApp.Application.Contracts;

public interface IAuthTokenManager
{
    public AuthTokenDto Generate(User user);
}