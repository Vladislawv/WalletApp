﻿using WalletApp.Application.Auth;
using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.Contracts;

public interface IAuthTokenManager
{
    public AuthToken Generate(User user);
}