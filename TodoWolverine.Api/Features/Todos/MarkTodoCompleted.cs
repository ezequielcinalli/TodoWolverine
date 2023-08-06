using Marten;
using Wolverine.Attributes;

namespace TodoWolverine.Api.Features.Todos;

public record MarkTodoCompleted(Guid Id) : IMutableTodo;

public record TodoCompleted(Guid Id);

public static class MarkTodoCompletedHandler
{
    [Transactional]
    public static async Task<TodoCompleted> HandleAsync(MarkTodoCompleted command, IDocumentSession documentSession)
    {
        var eventStream = await documentSession.Events.FetchForWriting<Todo>(command.Id);
        if (eventStream.Aggregate is null) throw new Exception("Todo not found");
        eventStream.AppendOne(new TodoCompleted(command.Id));
        var @event = new TodoCompleted(command.Id);
        return @event;
    }
}