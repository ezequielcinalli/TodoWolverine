namespace TodoWolverine.Api.Document.Tests.NSwag.Features.TodoFeatures;

public class GetTodosTests : BaseIntegrationTest
{
    public GetTodosTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetTodos_Should_Return_Empty_List_When_Database_Empty()
    {
        var listOfTodos = await NSwagClient.GetTodosAsync();
        listOfTodos.Should().BeEmpty();
    }
}