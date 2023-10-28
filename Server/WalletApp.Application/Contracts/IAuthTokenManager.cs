using System.Security.Claims;
using WalletApp.Application.Auth;
using WalletApp.Domain.Aggregates.UserAggregate;

namespace WalletApp.Application.Contracts;

public interface IAuthTokenManager
{
    public AuthToken Generate(User user);
    public IEnumerable<Claim> Decrypt(string token);
    public Guid ParseUserIdFromToken(string token);
}