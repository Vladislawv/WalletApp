using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Domain.TransactionAggregate;

namespace WalletApp.Application.Queries;

public record GetTransactionQuery(Guid Id) : IQuery<Transaction>;