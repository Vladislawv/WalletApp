using Microsoft.EntityFrameworkCore;
using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Application.Queries;
using WalletApp.Domain.Exceptions;
using WalletApp.Domain.TransactionAggregate;

namespace WalletApp.Application.QueryHandlers;

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