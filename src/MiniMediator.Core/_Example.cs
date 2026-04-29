using MiniMediator.Core.Abstractions;

namespace MiniMediator.Core;

public class CustomHandler : IRequestHandler<CustomCommand, int>
{
    public ValueTask<int> HandleAsync(CustomCommand request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}

public class CustomCommand : IRequest<int> { }


public class Test
{
    public Test(IMediator sender)
    {
        var test = sender.Send(new CustomCommand());
    }
}