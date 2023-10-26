using System.Net;

namespace WalletApp.Domain.Exceptions;

public abstract class WalletAppException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }

    protected WalletAppException(string message) : base(message) { }
}