using WalletApp.Application.CQRS.Abstractions;

namespace WalletApp.Application.CQRS.Commands;

public record CreateCardCommand(Guid UserId, Guid RequestedUserId) : ICommand;