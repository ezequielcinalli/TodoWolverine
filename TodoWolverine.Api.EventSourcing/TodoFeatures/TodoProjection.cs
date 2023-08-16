using Marten.Events.Aggregation;

namespace TodoWolverine.Api.EventSourcing.TodoFeatures;

public class TodoProjection : SingleStreamProjection<Todo>
{
    public void Apply(TodoCreated @event, Todo document)
    {
        document.Description = @event.Description;
        document.IsCompleted = false;
    }

    public void Apply(TodoCompleted @event, Todo document)
    {
        document.IsCompleted = true;
    }
}