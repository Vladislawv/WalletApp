using System.Net;

namespace WalletApp.Domain.Exceptions;

public class NotFoundException : WalletAppException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    
    public NotFoundException(string message) : base(message) { }
}