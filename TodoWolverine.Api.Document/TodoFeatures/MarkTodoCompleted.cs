namespace TodoWolverine.Api.Document.TodoFeatures;

public record MarkTodoCompleted(Guid Id) : IMutable<Todo>;

public record TodoCompleted(Guid Id) : IDomainEvent;

public static class MarkTodoCompletedHandler
{
    public const string TodoAlreadyCompleted = "Todo already completed";

    public static Result<Success> Handle(MarkTodoCompleted command, Todo todo, List<IDomainEvent> events)
    {
        if (todo.IsCompleted) return Result.Fail(TodoAlreadyCompleted);
        var @event = new TodoCompleted(command.Id);
        events.Add(@event);
        return Result.Ok();
    }
}