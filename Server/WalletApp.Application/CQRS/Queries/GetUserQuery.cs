using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Domain.Aggregates.UserAggregate;

namespace WalletApp.Application.CQRS.Queries;

public record GetUserQuery(Guid Id) : IQuery<User>;