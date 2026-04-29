namespace MiniMediator.Core.Abstractions;

public interface IValidator<TRequest>
{
    ValueTask ValidateAsync(TRequest request, CancellationToken ct);
}