using Microsoft.Extensions.DependencyInjection;
using MiniMediator.Core.Abstractions;

namespace MiniMediator.Core.Sender;

public sealed class Mediator(IServiceProvider provider) : IMediator
{
    private readonly IServiceProvider _provider = provider;

    public ValueTask<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
    {
        var handler = _provider.GetRequiredService<IRequestHandler<IRequest<TResponse>, TResponse>>();
        var pipelines = _provider.GetServices<IPipelineBehavior<IRequest<TResponse>, TResponse>>().ToList();

        RequestHandlerDelegate<TResponse> next = () => handler.HandleAsync(request, ct);

        for (int i = pipelines.Count - 1; i >= 0; i--)
        {
            var pipeline = pipelines[i];
            var current = next;

            next = () => pipeline.HandleAsync(request, ct, current);
        }

        return next();
    }
}