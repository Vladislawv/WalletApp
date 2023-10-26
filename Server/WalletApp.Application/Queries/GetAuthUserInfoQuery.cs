using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Application.Auth.Dto;

namespace WalletApp.Application.Queries;

public record GetAuthUserInfoQuery(string UserNameOrEmail, string Password) : IQuery<AuthUserInfoDto>;