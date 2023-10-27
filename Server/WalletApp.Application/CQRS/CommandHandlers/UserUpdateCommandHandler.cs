using Microsoft.AspNetCore.Identity;
using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Application.CQRS.Commands;
using WalletApp.Domain.Aggregates.UserAggregate;

namespace WalletApp.Application.CQRS.CommandHandlers;

public class UserUpdateCommandHandler : ICommandHandler<UserUpdateCommand>
{
    private readonly UserManager<User> _userManager;

    public UserUpdateCommandHandler(IUserRepository userRepository)
    {
        _userManager = userRepository.UserManager;
    }

    public async Task Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        await _userManager.UpdateAsync(request.User);
    }
}