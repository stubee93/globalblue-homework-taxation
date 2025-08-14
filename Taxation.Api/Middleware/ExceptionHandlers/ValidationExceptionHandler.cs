using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Taxation.Api.Middleware.ExceptionHandlers;

public class ValidationExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    private const string ValidationErrorsPropertyName = "errors";

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }
        
        // TODO logging
        
        var problemDetails = new ProblemDetails
        {
            Type = nameof(ValidationException),
            Status = StatusCodes.Status400BadRequest,
            Title = HttpStatusCode.BadRequest.ToString(),
            Detail = validationException.Message,
            Extensions =
            {
                [ValidationErrorsPropertyName] = validationException.Errors.Select(x => new
                {
                    x.PropertyName, x.ErrorMessage
                })
            }
        };
        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}