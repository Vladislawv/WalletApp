using Microsoft.EntityFrameworkCore;
using WalletApp.Domain.Aggregates.CardAggregate;
using WalletApp.Infrastructure.DataAccess.Database;

namespace WalletApp.Infrastructure.DataAccess.Repositories;

public class CardRepository : ICardRepository
{
    private readonly WalletAppContext _context;

    public CardRepository(WalletAppContext context)
    {
        _context = context;
    }

    public DbSet<Card> Cards => _context.Cards;
    public DbContext DbContext => _context;
}