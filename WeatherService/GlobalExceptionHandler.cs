using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using WeatherService.Exceptions;

namespace WeatherService;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        var response = httpContext.Response;

        var statusCode = (int)HttpStatusCode.InternalServerError;
        var errorMessage = "An unexpected error occurred.";

        switch (exception)
        {
            case NotFoundException notFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                errorMessage = notFoundException.Message;
                break;

            case ArgumentException argEx:
                statusCode = (int)HttpStatusCode.BadRequest;
                errorMessage = argEx.Message;
                break;

            default:
                logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);
                break;
        }

        response.StatusCode = statusCode;

        var problemDetails = new
        {
            status = statusCode,
            message = errorMessage
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);

        return true;
    }
}