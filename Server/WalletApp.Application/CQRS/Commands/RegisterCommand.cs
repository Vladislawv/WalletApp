using WalletApp.Application.CQRS.Abstractions;

namespace WalletApp.Application.CQRS.Commands;

public record RegisterCommand(string UserName, string Email, string Password) : ICommand;