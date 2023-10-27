using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Domain.Aggregates.TransactionAggregate;

namespace WalletApp.Application.CQRS.Queries;

public record GetTransactionQuery(Guid Id) : IQuery<Transaction>;