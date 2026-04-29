namespace MiniMediator.Core.Abstractions;

public interface IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    ValueTask<TResponse> HandleAsync(
        TRequest request,
        CancellationToken ct,
        RequestHandlerDelegate<TResponse> next);
}