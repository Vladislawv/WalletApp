using System.Net;

namespace WalletApp.Domain.Exceptions;

public class BadRequestException : WalletAppException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    
    public BadRequestException(string message) : base(message) { }
}