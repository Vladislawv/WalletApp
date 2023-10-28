using WalletApp.Domain.Aggregates.UserAggregate;

namespace WalletApp.Application.Auth;

public record AuthUserInfo(AuthToken Token, User User);