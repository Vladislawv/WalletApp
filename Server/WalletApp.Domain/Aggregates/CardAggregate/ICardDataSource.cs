namespace WalletApp.Domain.Aggregates.CardAggregate;

public interface ICardDataSource
{
    public IQueryable<Card> Cards { get; }
}