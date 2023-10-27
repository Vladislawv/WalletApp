using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Domain.CardAggregate;

namespace WalletApp.Application.Queries;

public record GetCardQuery(Guid Id) : IQuery<Card>;