using TodoWolverine.Api.Document.Tests.Fixtures;

namespace TodoWolverine.Api.Document.Tests.Features.TodoFeatures;

public class MarkTodoCompletedTests : BaseIntegrationTest
{
    public MarkTodoCompletedTests(WebAppFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task MarkTodoCompleted_Should_Return_Success()
    {
        var addTodoBody = new AddTodo("Todo 1");
        var addTodoResponse = await Host.PostJson(addTodoBody, TodosController.AddUrl).Receive<TodoCreated>();
        Assert.NotNull(addTodoResponse);

        var body = new MarkTodoCompleted(addTodoResponse.Id);
        await Host.Scenario(_ =>
        {
            _.Post.Json(body).ToUrl(TodosController.MarkCompletedUrl);
            _.StatusCodeShouldBeOk();
        });
    }
}