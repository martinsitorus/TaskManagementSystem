using System.Net;
using System.Text.Json;
using TaskManagementSystem.Domain.Exceptions;

namespace TaskManagementSystem.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (statusCode, title) = ex switch
        {
            TaskNotFoundException => (HttpStatusCode.NotFound, "Resource not found."),
            KeyNotFoundException => (HttpStatusCode.NotFound, "Resource not found."),
            UserAlreadyExistsException => (HttpStatusCode.Conflict, "Resource already exists."),
            ArgumentException => (HttpStatusCode.BadRequest, "Invalid request."),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            _logger.LogError(ex, "Unhandled exception processing {Method} {Path}.", context.Request.Method, context.Request.Path);
        }
        else
        {
            _logger.LogWarning(ex, "{Title} Processing {Method} {Path}.", title, context.Request.Method, context.Request.Path);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        var problem = new
        {
            title,
            status = (int)statusCode,
            detail = ex.Message
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
}
