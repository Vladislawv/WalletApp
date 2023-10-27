﻿using Microsoft.EntityFrameworkCore;
using WalletApp.Domain.CardAggregate;
using WalletApp.Infrastructure.DataAccess.Database;

namespace WalletApp.Infrastructure.DataAccess.DataSources;

public class CardDataSource : ICardDataSource
{
    private readonly WalletAppContext _context;

    public CardDataSource(WalletAppContext context)
    {
        _context = context;
    }

    public IQueryable<Card> Cards => _context.Cards.AsNoTracking();
}