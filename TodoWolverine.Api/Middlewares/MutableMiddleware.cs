using Marten.Events;
using Wolverine;

namespace TodoWolverine.Api.Middlewares;

public class MutableMiddleware<T> where T : class
{
    private readonly IDocumentSession _documentSession;
    private IEventStream<T>? _eventStream;

    public MutableMiddleware(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task<(HandlerContinuation, T, List<IDomainEvent> events)> BeforeAsync(IMutable<T> command,
        CancellationToken cancellationToken)
    {
        _eventStream = await _documentSession.Events.FetchForWriting<T>(command.Id, cancellationToken);
        if (_eventStream.Aggregate == null)
            throw new DomainException($"{typeof(T).Name} with id={command.Id} not found");

        return (HandlerContinuation.Continue, _eventStream.Aggregate, new List<IDomainEvent>());
    }

    public async Task AfterAsync(IMutable<T> command, List<IDomainEvent> events, CancellationToken cancellationToken)
    {
        if (!events.IsEmpty())
        {
            events.ForEach(e => { _eventStream?.AppendOne(e); });
            await _documentSession.SaveChangesAsync(cancellationToken);
        }
    }
}