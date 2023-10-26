using Microsoft.AspNetCore.Http;
using WalletApp.Domain.Common;

namespace WalletApp.Api.Middlewares;

public class TransactionIdMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var transactionId = Guid.NewGuid().ToString();
        using (CurrentTransaction.SetTransactionId(transactionId))
        {
            await next.Invoke(context);
        }
    }
}