using FluentResults;
using TodoWolverine.Api.Models;
using Wolverine.Attributes;

namespace TodoWolverine.Api.TodoFeatures;

public record MarkTodoCompleted(Guid Id) : IMutableTodo;

public record TodoCompleted(Guid Id) : IDomainEvent;

public static class MarkTodoCompletedHandler
{
    public const string TodoAlreadyCompleted = "Todo already completed";

    [Transactional]
    public static Result<Success> Handle(MarkTodoCompleted command, Todo todo, List<IDomainEvent> events)
    {
        if (todo.IsCompleted) return Result.Fail(TodoAlreadyCompleted);
        var @event = new TodoCompleted(command.Id);
        events.Add(@event);
        return Result.Ok();
    }
}