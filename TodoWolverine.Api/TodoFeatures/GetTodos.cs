using Marten;

namespace TodoWolverine.Api.TodoFeatures;

public record GetTodos;

public static class GetTodosHandler
{
    public static async Task<List<Todo>> HandleAsync(GetTodos query, IQuerySession querySession,
        CancellationToken cancellationToken)
    {
        var todos = await querySession.Query<Todo>().ToListAsync(cancellationToken);
        return todos.ToList();
    }
}