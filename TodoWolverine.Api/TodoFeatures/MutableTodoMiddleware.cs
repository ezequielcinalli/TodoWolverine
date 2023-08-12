using Marten;
using Marten.Events;
using TodoWolverine.Api.Models;
using Wolverine;

namespace TodoWolverine.Api.TodoFeatures;

public class MutableTodoMiddleware
{
    private readonly IDocumentSession _documentSession;
    private readonly ILogger<MutableTodoMiddleware> _logger;
    private IEventStream<Todo>? _eventStream;

    public MutableTodoMiddleware(IDocumentSession documentSession, ILogger<MutableTodoMiddleware> logger)
    {
        _documentSession = documentSession;
        _logger = logger;
    }

    public async Task<(HandlerContinuation, Todo, List<IDomainEvent> events)> BeforeAsync(IMutableTodo command,
        CancellationToken cancellationToken)
    {
        _eventStream = await _documentSession.Events.FetchForWriting<Todo>(command.Id, cancellationToken);
        if (_eventStream.Aggregate == null) throw new DomainException($"Todo with id={command.Id} not found");

        return (HandlerContinuation.Continue, _eventStream.Aggregate, new List<IDomainEvent>());
    }

    public async Task AfterAsync(IMutableTodo command, List<IDomainEvent> events, CancellationToken cancellationToken)
    {
        if (!events.IsEmpty())
        {
            events.ForEach(e => { _eventStream?.AppendOne(e); });
            await _documentSession.SaveChangesAsync(cancellationToken);
        }
    }
}