using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WalletApp.Application.Auth;
using WalletApp.Application.Contracts;
using WalletApp.Application.Options;
using WalletApp.Domain.UserAggregate;
using AuthTokenOptions = WalletApp.Application.Options.AuthOptions.TokenOptions;

namespace WalletApp.Infrastructure.Auth;

public class AuthJwtManager : IAuthTokenManager
{
    private readonly AuthTokenOptions _authTokenOptions;

    public AuthJwtManager(IOptions<AuthOptions> authOptions)
    {
        _authTokenOptions = authOptions.Value.Token;
    }

    public AuthToken Generate(User user)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authTokenOptions.Secret));
        var expirationDate = DateTimeOffset.UtcNow.AddSeconds(_authTokenOptions.ExpirationTimeSeconds);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = expirationDate.UtcDateTime,
            Issuer = _authTokenOptions.Issuer,
            Audience = _authTokenOptions.Audience,
            SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var authTokenValue = tokenHandler.WriteToken(token);

        return new AuthToken(authTokenValue, expirationDate);
    }

    public IEnumerable<Claim> Decrypt(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.ReadJwtToken(token).Claims;
    }

    public Guid ParseUserIdFromToken(string token)
    {
        var claims = Decrypt(token);
        var userIdClaim = claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) 
            ?? throw new Exception("Wrong token was provided.");
        
        return Guid.Parse(userIdClaim.Value);
    }
}