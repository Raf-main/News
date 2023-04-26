using System.Net;
using PRAS.Testovoe.Main.Exceptions;

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
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var errorMessage = exception.Message;
        switch(exception)
        {
            case NotFoundException ex:
                errorMessage = ex.Message;
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;       
                break;
        }

        await context.Response.WriteAsync(errorMessage);
    }
}