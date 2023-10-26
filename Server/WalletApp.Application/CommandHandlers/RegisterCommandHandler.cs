using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Application.Commands;
using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.CommandHandlers;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly IUserService _userService;

    public RegisterCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _userService.CreateAsync(request.UserName, request.Email, request.Password);
    }
}