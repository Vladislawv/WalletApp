using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Application.Auth;

namespace WalletApp.Application.Queries;

public record GetAuthUserInfoQuery(string UserNameOrEmail, string Password) : IQuery<AuthUserInfo>;