using Microsoft.Extensions.DependencyInjection;
using MiniMediator.Core.Abstractions;

namespace MiniMediator.Core;

public static class MiniMediatorServiceCollectionExtensions
{
    public static IServiceCollection AddMiniMediator(this IServiceCollection services)
    {
        services.AddSingleton<IMediator, Mediator>();
        return services;
    }

    public static IServiceCollection AddPipeline<TBehavior>(this IServiceCollection services)
        where TBehavior : class
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TBehavior));
        return services;
    }

    public static IServiceCollection AddHandlersFromAssembly(this IServiceCollection services, System.Reflection.Assembly assembly)
    {
        var handlers = assembly
            .GetTypes()
            .Where(t => !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .Select(i => new { Service = i, Implementation = t }));

        foreach (var h in handlers)
            services.AddTransient(h.Service, h.Implementation);

        return services;
    }

    public static IServiceCollection AddValidatorsFromAssembly(this IServiceCollection services, System.Reflection.Assembly assembly)
    {
        var validators = assembly
            .GetTypes()
            .Where(t => !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>))
                .Select(i => new { Service = i, Implementation = t }));

        foreach (var v in validators)
            services.AddTransient(v.Service, v.Implementation);

        return services;
    }
}