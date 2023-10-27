using Microsoft.AspNetCore.Identity;
using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Application.CQRS.Commands;
using WalletApp.Domain.Aggregates.UserAggregate;
using WalletApp.Domain.Exceptions;

namespace WalletApp.Application.CQRS.CommandHandlers;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly UserManager<User> _userManager;

    public RegisterCommandHandler(IUserRepository userRepository)
    {
        _userManager = userRepository.UserManager;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await ThrowIfUserExist(request);

        var user = BuildUser(request);
        var creationResult = await _userManager.CreateAsync(user, request.Password);
        ThrowIfCreationIsFail(creationResult);
    }

    private async Task ThrowIfUserExist(RegisterCommand request)
    {
        var isUserWithSameNameExists = await _userManager.FindByNameAsync(request.UserName) != null;
        var isUserWithSameEmailExists = await _userManager.FindByEmailAsync(request.Email) != null;
        
        if (isUserWithSameNameExists || isUserWithSameEmailExists)
        {
            throw new BadRequestException("User with the same email or username is already exists.");
        }
    }

    private static User BuildUser(RegisterCommand request)
    {
        return new User
        {
            UserName = request.UserName, Email = request.Email, DailyPoints = string.Empty, CreatedOn = DateTime.UtcNow
        };
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