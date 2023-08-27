namespace TodoWolverine.Api.Document.Tests.NSwag.Features.TodoFeatures;

public class MarkTodoCompletedTests : BaseIntegrationTest
{
    public MarkTodoCompletedTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task MarkTodoCompleted_Should_Return_Success()
    {
        var addTodoBody = new AddTodo { Description = "Todo 1" };

        var addTodoResponse = await NSwagClient.AddTodoAsync(addTodoBody);

        var body = new MarkTodoCompleted { Id = addTodoResponse.Id };
        await NSwagClient.MarkTodoCompletedAsync(body);
    }
}