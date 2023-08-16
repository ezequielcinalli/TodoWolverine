using TodoWolverine.Api.EventSourcing.Tests.Fixtures;
using TodoWolverine.Api.EventSourcing.TodoFeatures;

namespace TodoWolverine.Api.EventSourcing.Tests.Features.TodoFeatures;

public class AddTodoTests : BaseIntegrationTest
{
    public AddTodoTests(WebAppFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task AddTodo_Should_Return_TodoCreated_And_Add_To_Database()
    {
        var body = new AddTodo("Todo 1");
        var response = await Host.PostJson(body, TodosController.AddUrl).Receive<TodoCreated>();
        Assert.NotNull(response);
        response.Description.Should().Be(body.Description);

        var listOfTodos = await Host.GetAsJson<List<Todo>>(TodosController.GetAllUrl);
        Assert.NotNull(listOfTodos);
        listOfTodos.Should().HaveCount(1);
        listOfTodos.First().Id.Should().Be(response.Id);
        listOfTodos.First().Description.Should().Be(response.Description);
    }
}