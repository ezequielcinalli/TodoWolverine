using Marten;
using OneOf;
using OneOf.Types;
using Wolverine.Attributes;

namespace TodoWolverine.Api.Features.Todos;

public record MarkTodoCompleted(Guid Id) : IMutableTodo;

[GenerateOneOf]
public partial class MarkTodoCompletedResponse : OneOfBase<ResponseValidationError, Success>
{
}

public record TodoCompleted(Guid Id) : IDomainEvent;

public static class MarkTodoCompletedHandler
{
    [Transactional]
    public static MarkTodoCompletedResponse Handle(MarkTodoCompleted command, Todo todo,
        List<IDomainEvent> events, IDocumentSession documentSession)
    {
        //TODO: Return response validation error
        // if (todo.IsCompleted) throw new Exception("Todo is already completed");
        var @event = new TodoCompleted(command.Id);
        events.Add(@event);

        return new Success();
    }
}