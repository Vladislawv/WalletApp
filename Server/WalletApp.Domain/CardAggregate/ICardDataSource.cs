namespace WalletApp.Domain.CardAggregate;

public interface ICardDataSource
{
    public IQueryable<Card> Cards { get; }
}