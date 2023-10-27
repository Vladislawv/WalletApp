using Microsoft.AspNetCore.Http;
using WalletApp.Domain.Common;

namespace WalletApp.Api.Middlewares;

public class SessionIdMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var sessionId = Guid.NewGuid().ToString();
        using (CurrentSession.SetSessionId(sessionId))
        {
            await next.Invoke(context);
        }
    }
}