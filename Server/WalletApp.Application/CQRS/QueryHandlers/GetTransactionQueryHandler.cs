using Microsoft.EntityFrameworkCore;
using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Application.CQRS.Queries;
using WalletApp.Domain.Aggregates.TransactionAggregate;
using WalletApp.Domain.Exceptions;

namespace WalletApp.Application.CQRS.QueryHandlers;

public class GetTransactionQueryHandler : IQueryHandler<GetTransactionQuery, Transaction>
{
    private readonly ITransactionDataSource _transactionDataSource;

    public GetTransactionQueryHandler(ITransactionDataSource transactionDataSource)
    {
        _transactionDataSource = transactionDataSource;
    }

    public async Task<Transaction> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        return await _transactionDataSource.Transactions
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"Transaction with given Id: {request.Id} was not found.");
    }
}