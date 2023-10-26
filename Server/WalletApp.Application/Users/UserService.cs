using Microsoft.AspNetCore.Identity;
using WalletApp.Domain.Exceptions;
using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.Users;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> GetByCredentialsAsync(string userNameOrEmail, string password)
    {
        var user = await _userManager.FindByNameAsync(userNameOrEmail) 
                   ?? await _userManager.FindByEmailAsync(userNameOrEmail);

        if (user is null || !IsPasswordValid(user, password))
        {
            throw new BadRequestException("Given credentials are wrong.");
        }

        return user;
    }

    public async Task<User> CreateAsync(string userName, string email, string password)
    {
        await ThrowIfUserExist(userName, email);

        var user = new User { UserName = userName, Email = email };

        var creationResult = await _userManager.CreateAsync(user, password);
        ThrowIfCreationIsFail(creationResult);

        return user;
    }
    
    private bool IsPasswordValid(User user, string password)
    {
        var validationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return validationResult is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
    }

    private async Task ThrowIfUserExist(string userName, string email)
    {
        var isUserWithSameNameExists = await _userManager.FindByNameAsync(userName) != null;
        var isUserWithSameEmailExists = await _userManager.FindByEmailAsync(email) != null;
        
        if (isUserWithSameNameExists || isUserWithSameEmailExists)
        {
            throw new BadRequestException("User with the same email or username is already exists.");
        }
    }

    private static void ThrowIfCreationIsFail(IdentityResult creationResult)
    {
        if (creationResult.Succeeded)
        {
            return;
        }
        
        var errorMessage = string.Join("; ", creationResult.Errors);
        throw new BadRequestException(errorMessage);
    }
}