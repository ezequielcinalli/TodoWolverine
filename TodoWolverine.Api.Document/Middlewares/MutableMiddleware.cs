namespace TodoWolverine.Api.Document.Middlewares;

public class MutableMiddleware<T> where T : class
{
    private readonly IDocumentSession _documentSession;

    public MutableMiddleware(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public async Task<(HandlerContinuation, T)> BeforeAsync(IMutable<T> command,
        CancellationToken cancellationToken)
    {
        var document = await _documentSession.LoadAsync<T>(command.Id, cancellationToken);
        if (document == null) throw new DomainException($"{typeof(T).Name} with id={command.Id} not found");

        return (HandlerContinuation.Continue, document);
    }

    public async Task AfterAsync(IMutable<T> command, T document, CancellationToken cancellationToken)
    {
        _documentSession.Store(document);
        await _documentSession.SaveChangesAsync(cancellationToken);
    }
}