using ContestService.BLL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ContestService.API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleException(httpContext, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        logger.LogError(ex, "Unhandle exception");

        var (statusCode, message) = ex switch
        {
            NotFoundException  notFoundException=> (HttpStatusCode.NotFound, notFoundException.Message),
            BadRequestException badRequestException=> (HttpStatusCode.BadRequest, badRequestException.Message),
            _ => (HttpStatusCode.InternalServerError, ex.Message)
        };

        ProblemDetails details = new()
        {
            Status = (int)statusCode,
            Title = ex.Message,
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = details.Status.Value;

        await context.Response.WriteAsJsonAsync(details);
    }
}
