using Microsoft.EntityFrameworkCore;

namespace WalletApp.Domain.CardAggregate;

public interface ICardRepository
{
    public DbSet<Card> Cards { get; }
    public DbContext DbContext { get; }
}