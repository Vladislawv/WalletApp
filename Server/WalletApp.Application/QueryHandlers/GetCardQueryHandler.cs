using Microsoft.EntityFrameworkCore;
using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Application.Queries;
using WalletApp.Domain.CardAggregate;
using WalletApp.Domain.Exceptions;

namespace WalletApp.Application.QueryHandlers;

public class GetCardQueryHandler : IQueryHandler<GetCardQuery, Card>
{
    private readonly ICardDataSource _cardDataSource;

    public GetCardQueryHandler(ICardDataSource cardDataSource)
    {
        _cardDataSource = cardDataSource;
    }

    public async Task<Card> Handle(GetCardQuery request, CancellationToken cancellationToken)
    {
        return await _cardDataSource.Cards
            .Include(x => x.Transactions)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"Card with given Id: {request.Id} was not found.");
    }
}