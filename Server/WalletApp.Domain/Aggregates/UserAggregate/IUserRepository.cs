using Microsoft.AspNetCore.Identity;

namespace WalletApp.Domain.Aggregates.UserAggregate;

public interface IUserRepository
{
    public UserManager<User> UserManager { get; }
}