using Microsoft.AspNetCore.Identity;
using WalletApp.Domain.Aggregates.UserAggregate;

namespace WalletApp.Infrastructure.DataAccess.DataSources;

public class UserDataSource : IUserDataSource
{
    public UserDataSource(UserManager<User> userManager)
    {
        UserManager = userManager;
    }

    public UserManager<User> UserManager { get; }
}