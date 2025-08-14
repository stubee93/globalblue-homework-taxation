using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Taxation.Api.Middleware.ExceptionHandlers;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        // TODO logging
        
        var problemDetails = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Status = StatusCodes.Status500InternalServerError,
            Title = HttpStatusCode.InternalServerError.ToString(),
            Detail = "An error occurred while processing the request."
        };

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}