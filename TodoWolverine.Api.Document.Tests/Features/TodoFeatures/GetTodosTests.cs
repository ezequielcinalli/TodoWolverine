using TodoWolverine.Api.Document.Tests.Fixtures;

namespace TodoWolverine.Api.Document.Tests.Features.TodoFeatures;

public class GetTodosTests : BaseIntegrationTest
{
    public GetTodosTests(WebAppFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task GetTodos_Should_Return_Empty_List_When_Database_Empty()
    {
        var response = await Host.GetAsJson<List<Todo>>(TodosController.GetAllUrl);
        Assert.NotNull(response);
        response.Should().BeEmpty();
    }
}