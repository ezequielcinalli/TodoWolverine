using Alba;
using FluentAssertions;
using TodoWolverine.Api.Tests.Fixtures;
using TodoWolverine.Api.TodoFeatures;

namespace TodoWolverine.Api.Tests;

public class TodosEndpointsTests : BaseIntegrationTest
{
    public TodosEndpointsTests(WebAppFixture fixture) : base(fixture)
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

    [Fact]
    public async Task GetTodos_Should_Return_Empty_List_When_Database_Empty()
    {
        var body = new GetTodos();
        var response = await Host.GetAsJson<List<Todo>>(TodosController.GetAllUrl);
        Assert.NotNull(response);
        response.Should().BeEmpty();
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