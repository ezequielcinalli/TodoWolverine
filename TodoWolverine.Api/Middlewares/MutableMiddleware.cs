﻿using Marten;
using Marten.Events;
using TodoWolverine.Api.Models;
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

    public async Task<(HandlerContinuation, T, List<IDomainEvent> events)> BeforeAsync(IGuid command,
        CancellationToken cancellationToken)
    {
        _eventStream = await _documentSession.Events.FetchForWriting<T>(command.Id, cancellationToken);
        if (_eventStream.Aggregate == null)
            throw new DomainException($"{typeof(T).Name} with id={command.Id} not found");

        return (HandlerContinuation.Continue, _eventStream.Aggregate, new List<IDomainEvent>());
    }

    public async Task AfterAsync(IGuid command, List<IDomainEvent> events, CancellationToken cancellationToken)
    {
        if (!events.IsEmpty())
        {
            events.ForEach(e => { _eventStream?.AppendOne(e); });
            await _documentSession.SaveChangesAsync(cancellationToken);
        }
    }
}