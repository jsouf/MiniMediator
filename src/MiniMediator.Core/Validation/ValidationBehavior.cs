using MiniMediator.Core.Abstractions;

namespace MiniMediator.Core.Validation;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async ValueTask<TResponse> HandleAsync(
        TRequest request,
        CancellationToken ct,
        RequestHandlerDelegate<TResponse> next)
    {
        foreach (var v in _validators)
            await v.ValidateAsync(request, ct);

        return await next();
    }
}