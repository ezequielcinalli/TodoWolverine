namespace TodoWolverine.Api.Document.Tests.NSwag.Features.TodoFeatures;

public class AddTodoTests : BaseIntegrationTest
{
    public AddTodoTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task AddTodo_Should_Return_TodoCreated_And_Add_To_Database()
    {
        var body = AutoFixture.Create<AddTodo>();
        var response = await NSwagClient.AddTodoAsync(body);
        response.Id.Should().NotBeEmpty();
        response.Description.Should().Be(body.Description);

        var listOfTodos = await NSwagClient.GetTodosAsync();
        // var querySession = Scope.ServiceProvider.GetRequiredService<IQuerySession>();
        // var listOfTodos = await querySession.Query<Todo>().ToListAsync();
        listOfTodos.Should().HaveCount(1);
        listOfTodos.First().Id.Should().Be(response.Id);
        listOfTodos.First().Description.Should().Be(response.Description);
    }
}