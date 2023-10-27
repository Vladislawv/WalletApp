﻿using Shared.InternalMessaging.CQRS.Abstractions;
using WalletApp.Application.Queries;
using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.QueryHandlers;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, User>
{
    private readonly IUserService _userService;

    public GetUserQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await _userService.GetByIdAsync(request.Id);
    }
}