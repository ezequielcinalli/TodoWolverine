using Marten;
using TodoWolverine.Api.Models;
using Wolverine;

namespace TodoWolverine.Api.Middlewares;

public class NewEventStreamMiddleware
{
    private readonly IDocumentSession _documentSession;

    public NewEventStreamMiddleware(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public (HandlerContinuation, List<IDomainEvent> events) Before(NewEventStream command,
        CancellationToken cancellationToken)
    {
        return (HandlerContinuation.Continue, new List<IDomainEvent>());
    }

    public async Task AfterAsync(NewEventStream command, List<IDomainEvent> events, CancellationToken cancellationToken)
    {
        if (!events.IsEmpty())
        {
            events.ForEach(e => { _documentSession.Events.Append(e.Id, e); });
            await _documentSession.SaveChangesAsync(cancellationToken);
        }
    }
}