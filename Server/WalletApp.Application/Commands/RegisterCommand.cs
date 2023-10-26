using Shared.InternalMessaging.CQRS.Abstractions;

namespace WalletApp.Application.Commands;

public record RegisterCommand(string UserName, string Email, string Password) : ICommand;