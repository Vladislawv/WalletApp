using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Application.CQRS.Commands;
using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.CQRS.CommandHandlers;

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