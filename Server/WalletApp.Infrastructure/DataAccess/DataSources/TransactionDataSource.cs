﻿using Microsoft.EntityFrameworkCore;
using WalletApp.Domain.Aggregates.TransactionAggregate;

namespace WalletApp.Infrastructure.DataAccess.DataSources;

public class TransactionDataSource : ITransactionDataSource
{
    private readonly WalletAppContext _context;

    public TransactionDataSource(WalletAppContext context)
    {
        _context = context;
    }

    public IQueryable<Transaction> Transactions => _context.Transactions.AsNoTracking();
}