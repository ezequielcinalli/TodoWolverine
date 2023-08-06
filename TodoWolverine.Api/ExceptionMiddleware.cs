using FluentValidation;

namespace TodoWolverine.Api;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
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
            httpContext.Response.StatusCode = 500;
            httpContext.Response.Headers.TryAdd("Exception", "yes");
            var error = new ResponseExceptionError();
            await httpContext.Response.WriteAsJsonAsync(error);
        }
    }
}