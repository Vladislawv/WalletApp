using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Domain.Aggregates.UserAggregate;

namespace WalletApp.Application.CQRS.Commands;

public record UserUpdateCommand(User User) : ICommand;