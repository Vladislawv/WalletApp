﻿using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Application.Auth;
using WalletApp.Application.Contracts;
using WalletApp.Application.Queries;
using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.QueryHandlers;

public class GetAuthUserInfoQueryHandler : IQueryHandler<GetAuthUserInfoQuery, AuthUserInfo>
{
    private readonly IUserService _userService;
    private readonly IAuthTokenManager _authTokenManager;

    public GetAuthUserInfoQueryHandler(IUserService userService, IAuthTokenManager authTokenManager)
    {
        _userService = userService;
        _authTokenManager = authTokenManager;
    }

    public async Task<AuthUserInfo> Handle(GetAuthUserInfoQuery getUserQuery, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var user = await _userService.GetByCredentialsAsync(getUserQuery.UserNameOrEmail, getUserQuery.Password);
        var token = _authTokenManager.Generate(user);

        return new AuthUserInfo(token, user);
    }
}