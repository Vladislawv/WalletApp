using WalletApp.Application.Auth;
using WalletApp.Application.CQRS.Abstractions;

namespace WalletApp.Application.CQRS.Queries;

public record GetAuthUserInfoQuery(string UserNameOrEmail, string Password) : IQuery<AuthUserInfo>;