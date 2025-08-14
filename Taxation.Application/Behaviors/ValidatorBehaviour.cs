using FluentValidation;
using MediatR;

namespace Taxation.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        switch (validators.Count())
        {
            case 0:
                return await next(cancellationToken);
            case 1:
                await validators.First().ValidateAndThrowAsync(request, cancellationToken);
                return await next(cancellationToken);
            case > 1:
                throw new NotSupportedException("Only one validator is allowed per request");
            default:
                throw new InvalidOperationException("Should never reach this point");
        }
    }
}