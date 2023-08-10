using FluentResults;
using Microsoft.AspNetCore.Mvc;
using TodoWolverine.Api.Extensions;
using Wolverine;

namespace TodoWolverine.Api.TodoFeatures;

[ApiController]
[Route("[controller]")]
public class TodosController
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<Todo>))]
    public async Task<IActionResult> GetTodos(IMessageBus messageBus)
    {
        var response = await messageBus.InvokeAsync<Result<List<Todo>>>(new GetTodos());
        return response.ToHttpResponse();
    }

    [HttpPost("Add")]
    [ProducesResponseType(200, Type = typeof(Todo))]
    public async Task<IActionResult> AddTodo([FromBody] AddTodo request, IMessageBus messageBus)
    {
        var response = await messageBus.InvokeAsync<Result<Todo>>(request);
        return response.ToHttpResponse();
    }

    [HttpPost("MarkCompleted")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> MarkTodoCompleted([FromBody] MarkTodoCompleted request, IMessageBus messageBus)
    {
        var response = await messageBus.InvokeAsync<Result<Success>>(request);
        return response.ToHttpResponse();
    }
}