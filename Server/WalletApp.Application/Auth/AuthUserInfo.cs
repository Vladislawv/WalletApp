using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.Auth;

public record AuthUserInfo(AuthToken Token, User User);