using System.Net;
using System.Net.Mime;
using System.Text.Json;
using WalletApi.Domain.Common;
using WalletApi.Domain.Exceptions;
using WalletApp.Api.Dto;

namespace WalletApp.Api.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private const string INTERNAL_SERVER_ERROR_MESSAGE = "Internal server error.";
    private static readonly JsonSerializerOptions JSON_WEB_SERIALIZER_OPTIONS = new(JsonSerializerDefaults.Web);
    
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(IWebHostEnvironment environment, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex, context);
            _logger.LogError(ex, "Exception was thrown: {message}, StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
        }
    }

    private async Task HandleExceptionAsync(Exception ex, HttpContext context)
    {
        var response = context.Response;
        response.ContentType = MediaTypeNames.Application.Json;

        var errorDto = new ErrorDto { TransactionId = CurrentTransaction.TransactionId };

        if (ex is WalletAppException exception)
        {
            response.StatusCode = (int)exception.StatusCode;
            errorDto.ErrorMessage = exception.Message;
        }
        else
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            errorDto.ErrorMessage = INTERNAL_SERVER_ERROR_MESSAGE;
        }

        if (_environment.IsDevelopment())
        {
            errorDto.StackTrace = ex.StackTrace;
        }

        await response.WriteAsJsonAsync(errorDto, JSON_WEB_SERIALIZER_OPTIONS);
    }
}