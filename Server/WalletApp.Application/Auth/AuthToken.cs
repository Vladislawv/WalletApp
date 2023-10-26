namespace WalletApp.Application.Auth;

public record AuthToken(string Value, DateTimeOffset ExpirationDate);