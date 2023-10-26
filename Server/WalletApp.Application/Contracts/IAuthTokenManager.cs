using WalletApp.Application.Auth.Dto;
using WalletApp.DAL.Models.Users;

namespace WalletApp.Application.Contracts;

public interface IAuthTokenManager
{
    public AuthTokenDto Generate(User user);
}