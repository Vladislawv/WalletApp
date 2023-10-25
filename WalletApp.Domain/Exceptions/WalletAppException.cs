using System.Net;

namespace WalletApi.Domain.Exceptions;

public abstract class WalletAppException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }

    protected WalletAppException(string message) : base(message) { }
}