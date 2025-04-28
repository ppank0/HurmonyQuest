using ContestService.BLL.Exceptions;
using Microsoft.AspNetCore.Mvc;
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
        catch (NotFoundException ex)
        {
            await HandleException(httpContext, ex, StatusCodes.Status404NotFound);
        }
        catch (BadRequestException ex)
        {
            await HandleException(httpContext, ex, StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            await HandleException(httpContext, ex, StatusCodes.Status500InternalServerError);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex, int statusCode)
    {
        logger.LogError(ex, "Unhandle exception");

        ProblemDetails details = new()
        {
            Status = statusCode,
            Title = ex.Message,
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = details.Status.Value;

        string jsonProblem = JsonSerializer.Serialize(details);

        await context.Response.WriteAsync(jsonProblem);
    }
}
