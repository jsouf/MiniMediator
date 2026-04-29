```


namespace MiniMediator.Generator;

[Generator]
public class MediatorGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Scan IRequestHandler<,>
        // Generate optimized dispatch switch
        // Optional bypass of DI for hot paths
    }
}

services.AddMiniMediator();
services.AddPipeline<LoggingBehavior<,>>();
services.AddPipeline<ValidationBehavior<,>>();

services.AddHandlersFromAssembly(typeof(SomeHandler).Assembly);
services.AddValidatorsFromAssembly(typeof(SomeValidator).Assembly);


```