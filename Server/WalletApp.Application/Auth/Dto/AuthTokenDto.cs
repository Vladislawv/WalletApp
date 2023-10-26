namespace WalletApp.Application.Auth.Dto;

public record AuthTokenDto(string Value, DateTimeOffset ExpirationDate);