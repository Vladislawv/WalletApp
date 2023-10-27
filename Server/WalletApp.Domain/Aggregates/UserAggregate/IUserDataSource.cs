using Microsoft.AspNetCore.Identity;

namespace WalletApp.Domain.Aggregates.UserAggregate;

public interface IUserDataSource
{
    public UserManager<User> UserManager { get; }
}