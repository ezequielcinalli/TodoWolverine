using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace TodoWolverine.Api.Features.Todos;

[ApiController]
[Route("[controller]")]
public class TodosController
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<Todo>))]
    public async Task<IActionResult> GetTodos(IMessageBus messageBus)
    {
        var todos = await messageBus.InvokeAsync<List<Todo>>(new GetTodos());
        return new OkObjectResult(todos);
    }

    [HttpPost("Add")]
    [ProducesResponseType(200, Type = typeof(TodoCreated))]
    public async Task<IActionResult> AddTodo([FromBody] AddTodo request, IMessageBus messageBus)
    {
        var todo = await messageBus.InvokeAsync<TodoCreated>(request);
        return new OkObjectResult(todo);
    }

    [HttpPost("MarkCompleted")]
    [ProducesResponseType(200, Type = typeof(TodoCompleted))]
    public async Task<IActionResult> MarkTodoCompleted([FromBody] MarkTodoCompleted request, IMessageBus messageBus)
    {
        var todo = await messageBus.InvokeAsync<TodoCompleted>(request);
        return new OkObjectResult(todo);
    }
}