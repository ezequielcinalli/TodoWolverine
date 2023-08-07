using OneOf;
using OneOf.Types;
using TodoWolverine.Api.Models;
using Wolverine.Attributes;

namespace TodoWolverine.Api.TodoFeatures;

public record MarkTodoCompleted(Guid Id) : IMutableTodo;

[GenerateOneOf]
public partial class MarkTodoCompletedResponse : OneOfBase<ResponseValidationError, Success>
{
}

public record TodoCompleted(Guid Id) : IDomainEvent;

public static class MarkTodoCompletedHandler
{
    public const string TodoAlreadyCompleted = "Todo already completed";

    [Transactional]
    public static MarkTodoCompletedResponse Handle(MarkTodoCompleted command, Todo todo, List<IDomainEvent> events)
    {
        if (todo.IsCompleted) return new ResponseValidationError(TodoAlreadyCompleted);
        var @event = new TodoCompleted(command.Id);
        events.Add(@event);
        return new Success();
    }
}