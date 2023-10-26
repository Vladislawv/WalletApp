using WalletApp.Domain.UserAggregate;

namespace WalletApp.Application.Auth.Dto;

public record AuthUserInfoDto(AuthTokenDto Token, User User);