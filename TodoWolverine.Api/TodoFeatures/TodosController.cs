using Microsoft.AspNetCore.Mvc;
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
        var todos = await messageBus.InvokeAsync<List<Todo>>(new GetTodos());
        return new OkObjectResult(todos);
    }

    [HttpPost("Add")]
    [ProducesResponseType(200, Type = typeof(Todo))]
    public async Task<IActionResult> AddTodo([FromBody] AddTodo request, IMessageBus messageBus)
    {
        var response = await messageBus.InvokeAsync<AddTodoResponse>(request);
        return response.Match<IActionResult>(
            validationError => new BadRequestObjectResult(validationError),
            todo => new OkObjectResult(todo)
        );
    }

    [HttpPost("MarkCompleted")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> MarkTodoCompleted([FromBody] MarkTodoCompleted request, IMessageBus messageBus)
    {
        var response = await messageBus.InvokeAsync<MarkTodoCompletedResponse>(request);
        return response.Match<IActionResult>(
            validationError => new BadRequestObjectResult(validationError),
            success => new OkResult()
        );
    }
}