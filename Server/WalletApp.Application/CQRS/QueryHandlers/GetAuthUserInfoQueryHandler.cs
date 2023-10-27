using Microsoft.AspNetCore.Identity;
using WalletApp.Application.Auth;
using WalletApp.Application.Contracts;
using WalletApp.Application.CQRS.Abstractions;
using WalletApp.Application.CQRS.Queries;
using WalletApp.Domain.Aggregates.UserAggregate;
using WalletApp.Domain.Exceptions;

namespace WalletApp.Application.CQRS.QueryHandlers;

public class GetAuthUserInfoQueryHandler : IQueryHandler<GetAuthUserInfoQuery, AuthUserInfo>
{
    private readonly IUserDataSource _userDataSource;
    private readonly IAuthTokenManager _authTokenManager;

    public GetAuthUserInfoQueryHandler(IUserDataSource userDataSource, IAuthTokenManager authTokenManager)
    {
        _userDataSource = userDataSource;
        _authTokenManager = authTokenManager;
    }

    public async Task<AuthUserInfo> Handle(GetAuthUserInfoQuery query, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await GetByQueryAsync(query);
        var token = _authTokenManager.Generate(user);
        return new AuthUserInfo(token, user);
    }

    private async Task<User> GetByQueryAsync(GetAuthUserInfoQuery query)
    {
        var user = await _userDataSource.UserManager.FindByNameAsync(query.UserNameOrEmail)
                   ?? await _userDataSource.UserManager.FindByEmailAsync(query.UserNameOrEmail);

        if (user is null || !IsPasswordValid(user, query.Password))
        {
            throw new BadRequestException("Given credentials are wrong.");
        }

        return user;
    }

    private bool IsPasswordValid(User user, string password)
    {
        var validationResult = _userDataSource.UserManager.PasswordHasher
            .VerifyHashedPassword(user, user.PasswordHash, password);

        return validationResult is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
    }
}