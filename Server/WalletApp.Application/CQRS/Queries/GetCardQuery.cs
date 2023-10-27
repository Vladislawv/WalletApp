using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Domain.CardAggregate;

namespace WalletApp.Application.CQRS.Queries;

public record GetCardQuery(Guid Id) : IQuery<Card>;