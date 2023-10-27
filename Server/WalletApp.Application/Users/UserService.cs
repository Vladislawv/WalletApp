using Microsoft.AspNetCore.Identity;
using WalletApp.Domain.DailyPointAggregate;
using WalletApp.Domain.Exceptions;
using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.Users;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IDailyPointCalculationService _dailyPointCalculationService;

    public UserService(UserManager<User> userManager, IDailyPointCalculationService dailyPointCalculationService)
    {
        _userManager = userManager;
        _dailyPointCalculationService = dailyPointCalculationService;
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

        var user = BuildUser(userName, email);
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

    private User BuildUser(string userName, string email)
    {
        var initialPoints = _dailyPointCalculationService.GetInitialPoints();
        return new User { UserName = userName, Email = email, DailyPoints = initialPoints, CreatedOn = DateTime.UtcNow };
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