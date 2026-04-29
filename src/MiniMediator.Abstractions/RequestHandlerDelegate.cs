namespace MiniMediator.Abstractions;

public delegate ValueTask<TResponse> RequestHandlerDelegate<TResponse>();
