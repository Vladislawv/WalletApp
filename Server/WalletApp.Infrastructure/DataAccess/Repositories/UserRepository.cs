using Microsoft.AspNetCore.Identity;
using WalletApp.Domain.Aggregates.UserAggregate;

namespace WalletApp.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    public UserRepository(UserManager<User> userManager)
    {
        UserManager = userManager;
    }

    public UserManager<User> UserManager { get; }
}