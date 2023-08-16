using Microsoft.AspNetCore.Mvc;
using TodoWolverine.Api.EventSourcing.Extensions;
using Wolverine;

namespace TodoWolverine.Api.EventSourcing.TodoFeatures;

[ApiController]
public class TodosController
{
    public const string GetAllUrl = "/todos";
    public const string AddUrl = "/todos/add";
    public const string MarkCompletedUrl = "/todos/markcompleted";

    [HttpGet(GetAllUrl, Name = nameof(GetTodos))]
    [ProducesResponseType(200, Type = typeof(IReadOnlyList<Todo>))]
    public async Task<IActionResult> GetTodos(IQuerySession querySession, CancellationToken cancellationToken)
    {
        var response = await querySession.Query<Todo>().ToJsonArray(cancellationToken);
        return response.ToHttpResponse();
    }

    [HttpPost(AddUrl, Name = nameof(AddTodo))]
    [ProducesResponseType(200, Type = typeof(Todo))]
    public async Task<IActionResult> AddTodo([FromBody] AddTodo request, IMessageBus messageBus,
        CancellationToken cancellationToken)
    {
        var response = await messageBus.InvokeAsync<Result<Todo>>(request, cancellationToken);
        return response.ToHttpResponse();
    }

    [HttpPost(MarkCompletedUrl, Name = nameof(MarkTodoCompleted))]
    [ProducesResponseType(200)]
    public async Task<IActionResult> MarkTodoCompleted([FromBody] MarkTodoCompleted request, IMessageBus messageBus,
        CancellationToken cancellationToken)
    {
        var response = await messageBus.InvokeAsync<Result<Success>>(request, cancellationToken);
        return response.ToHttpResponse();
    }
}