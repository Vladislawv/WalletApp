using Microsoft.EntityFrameworkCore;

namespace WalletApp.Domain.Aggregates.CardAggregate;

public interface ICardRepository
{
    public DbSet<Card> Cards { get; }
    public DbContext DbContext { get; }
}