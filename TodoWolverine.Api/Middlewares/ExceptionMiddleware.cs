using FluentValidation;
using TodoWolverine.Api.Models;

namespace TodoWolverine.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            httpContext.Response.StatusCode = 400;
            var error = new ResponseValidationError(ex.Errors.Select(x => x.ErrorMessage).ToList());
            await httpContext.Response.WriteAsJsonAsync(error);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception catch in ExceptionMiddleware");
            httpContext.Response.StatusCode = 500;
            httpContext.Response.Headers.TryAdd("Exception", "yes");
            var error = new ResponseExceptionError();
            await httpContext.Response.WriteAsJsonAsync(error);
        }
    }
}