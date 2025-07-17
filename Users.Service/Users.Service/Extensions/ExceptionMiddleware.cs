using Microsoft.AspNetCore.Mvc;
using System.Net;
using UsersService.Domain.Exceptions;

namespace UsersService.API.Extensions
{
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

        private async Task HandleException(HttpContext httpContext, Exception exception)
        {
            logger.LogError(exception, "Unhandle exception");

            var (statusCode, message) = exception switch
            {
                NotFoundException => (HttpStatusCode.NotFound, exception.Message),
                BadRequestException => (HttpStatusCode.BadRequest, exception.Message),
                ConflictException => (HttpStatusCode.Conflict, exception.Message),
                _ => (HttpStatusCode.InternalServerError, exception.Message)
            };

            ProblemDetails details = new()
            {
                Status = (int)statusCode,
                Title = message,
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = details.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(details);
        }
    }
}
