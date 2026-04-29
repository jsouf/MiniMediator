namespace MiniMediator.Abstractions;

public interface IMediator
{
    ValueTask<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}
