using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.Queries;

public record GetUserQuery(Guid Id) : IQuery<User>;