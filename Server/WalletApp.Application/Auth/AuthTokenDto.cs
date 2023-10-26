namespace WalletApp.Application.Auth;

public record AuthTokenDto(string Value, DateTimeOffset ExpirationDate);