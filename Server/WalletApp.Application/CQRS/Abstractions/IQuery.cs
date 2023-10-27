using MediatR;

namespace WalletApp.Application.CQRS.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}