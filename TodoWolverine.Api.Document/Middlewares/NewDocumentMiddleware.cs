namespace TodoWolverine.Api.Document.Middlewares;

public class NewDocumentMiddleware<T> where T : class
{
    private readonly IDocumentSession _documentSession;

    public NewDocumentMiddleware(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    public (HandlerContinuation, List<T> documents) Before(INewDocument<T> command,
        CancellationToken cancellationToken)
    {
        return (HandlerContinuation.Continue, new List<T>());
    }

    public async Task AfterAsync(INewDocument<T> command, List<T> documents, CancellationToken cancellationToken)
    {
        if (documents.Any())
        {
            documents.ForEach(d => _documentSession.Store(d));
            await _documentSession.SaveChangesAsync(cancellationToken);
        }
    }
}