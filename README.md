# 📘 MiniMediator

> A lightweight, MediatR-like mediator for .NET 8 with optional compile-time Source Generator for ultra-fast dispatch.

---

## 🚀 Overview

MiniMediator is a simple and high-performance mediator implementation for .NET 8.

It provides a CQRS-style architecture with:

- Commands / Queries
- Pipeline behaviors (like MediatR)
- Validation support
- Dependency Injection integration
- Optional Source Generator for compile-time dispatch optimization

> Developer experience first — performance second — zero unnecessary complexity.

---

## ✨ Features

- Clean MediatR-like API
- CQRS support (`IRequest<T>`)
- Pipeline behaviors (logging, validation, etc.)
- Fluent DI registration
- Auto-discovered handlers
- Auto-discovered validators
- ASP.NET Core DI compatible
- Optional Source Generator
- No reflection in hot path (with generator enabled)

---

## 📦 Installation

### Core package

```bash
dotnet add package MiniMediator
```

### Optional Source Generator

```bash
dotnet add package MiniMediator.Generator
```

---

## 🧠 Core Concepts

### IRequest

```csharp
public interface IRequest<TResponse> { }
```

Defines a request returning a response.

---

### Handler

```csharp
public record GetUserQuery(int Id) : IRequest<string>;

public class GetUserHandler : IRequestHandler<GetUserQuery, string>
{
    public ValueTask<string> HandleAsync(GetUserQuery request, CancellationToken ct)
    {
        return ValueTask.FromResult($"User {request.Id}");
    }
}
```

---

## 📡 Mediator usage

```csharp
var result = await mediator.Send<GetUserQuery, string>(
    new GetUserQuery(1),
    cancellationToken);
```

---

## 🔧 Pipeline Behaviors

Pipeline behaviors allow cross-cutting concerns.

### Logging example

```csharp
public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    public async ValueTask<TResponse> HandleAsync(
        TRequest request,
        CancellationToken ct,
        RequestHandlerDelegate<TResponse> next)
    {
        Console.WriteLine($"Handling {typeof(TRequest).Name}");
        return await next();
    }
}
```

---

## ✅ Validation

### Validator

```csharp
public class GetUserValidator : IValidator<GetUserQuery>
{
    public ValueTask ValidateAsync(GetUserQuery request, CancellationToken ct)
    {
        if (request.Id <= 0)
            throw new ArgumentException("Invalid Id");

        return ValueTask.CompletedTask;
    }
}
```

### Used automatically via pipeline

```csharp
services.AddPipeline<ValidationBehavior<,>>();
```

---

## ⚙️ Dependency Injection

### Setup

```csharp
services.AddMiniMediator();
```

### Register handlers

```csharp
services.AddHandlersFromAssembly(typeof(Program).Assembly);
```

### Register validators

```csharp
services.AddValidatorsFromAssembly(typeof(Program).Assembly);
```

### Register pipelines

```csharp
services.AddPipeline<LoggingBehavior<,>>();
services.AddPipeline<ValidationBehavior<,>>();
```

---

## ⚡ Source Generator (optional)

MiniMediator includes an optional Roslyn Source Generator.

### What it does

- Generates request → handler mapping at compile time
- Removes runtime reflection from dispatch
- Optimizes execution path

---

### Example generated code

```csharp
if (request is GetUserQuery q)
{
    var handler = new GetUserHandler();
    return handler.HandleAsync(q, ct);
}
```

---

## 🧪 Performance

| Mode | Performance | Flexibility |
|------|------------|-------------|
| Standard DI pipeline | ⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| With Source Generator | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ |

---

## 🏗 Architecture

MiniMediator
- Abstractions → Contracts (no dependencies)
- Runtime → Mediator + pipeline engine
- Generator → Compile-time optimization (optional)

---

## 💡 Design Philosophy

- Simple API like MediatR
- Clean Architecture friendly
- Minimal runtime overhead
- Fully DI compatible
- Optional compile-time optimization

---

## 📌 Example full setup

```csharp
services.AddMiniMediator();

services.AddPipeline<LoggingBehavior<,>>();
services.AddPipeline<ValidationBehavior<,>>();

services.AddHandlersFromAssembly(typeof(GetUserHandler).Assembly);
services.AddValidatorsFromAssembly(typeof(GetUserValidator).Assembly);
```

---

## 🧠 When to use the Generator?

### ✔ Use it if you want:
- Maximum performance
- Zero reflection dispatch
- Compile-time safety

### ❌ Skip it if you prefer:
- Maximum flexibility
- Pure DI runtime behavior

---

## 📜 License

MIT

---

## 🚀 Roadmap

- Incremental generator v2
- Pipeline caching optimization
- AOT / Native compatibility improvements
- Benchmark vs MediatR
- Analyzer for missing handlers

---

## 🤝 Contributing

PRs welcome:
- Performance improvements
- Generator enhancements
- API polish
