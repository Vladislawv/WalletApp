using Shared.InternalMessaging.CQRS.Abstractions;

namespace WalletApp.Application.Commands;

public record CreateCardCommand(Guid UserId, Guid RequestedUserId) : ICommand;