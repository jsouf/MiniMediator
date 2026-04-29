namespace MiniMediator.Core.Abstractions;

public delegate ValueTask<TResponse> RequestHandlerDelegate<TResponse>();
