using MiniMediator.Abstractions;

namespace MiniMediator.Core.Behaviors;

internal sealed class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async ValueTask<TResponse> HandleAsync(
        TRequest request,
        CancellationToken ct,
        RequestHandlerDelegate<TResponse> next)
    {
        Console.WriteLine($"[MiniMediator-LOG] {typeof(TRequest).Name}");
        return await next();
    }
}
