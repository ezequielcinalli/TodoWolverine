using FluentResults;
using Microsoft.AspNetCore.Mvc;
using TodoWolverine.Api.Extensions;
using Wolverine;

namespace TodoWolverine.Api.TodoFeatures;

[ApiController]
public class TodosController
{
    public const string GetAllUrl = "/todos";
    public const string AddUrl = "/todos/add";
    public const string MarkCompletedUrl = "/todos/markcompleted";

    [HttpGet(GetAllUrl)]
    [ProducesResponseType(200, Type = typeof(List<Todo>))]
    public async Task<IActionResult> GetTodos(IMessageBus messageBus)
    {
        var response = await messageBus.InvokeAsync<Result<List<Todo>>>(new GetTodos());
        return response.ToHttpResponse();
    }

    [HttpPost(AddUrl)]
    [ProducesResponseType(200, Type = typeof(Todo))]
    public async Task<IActionResult> AddTodo([FromBody] AddTodo request, IMessageBus messageBus)
    {
        var response = await messageBus.InvokeAsync<Result<Todo>>(request);
        return response.ToHttpResponse();
    }

    [HttpPost(MarkCompletedUrl)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> MarkTodoCompleted([FromBody] MarkTodoCompleted request, IMessageBus messageBus)
    {
        var response = await messageBus.InvokeAsync<Result<Success>>(request);
        return response.ToHttpResponse();
    }
}