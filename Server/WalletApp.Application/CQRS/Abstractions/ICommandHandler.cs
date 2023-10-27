using MediatR;

namespace WalletApp.Application.CQRS.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}