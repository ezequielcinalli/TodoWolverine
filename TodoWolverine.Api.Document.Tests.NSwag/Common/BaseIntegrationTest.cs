using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace TodoWolverine.Api.Document.Tests.NSwag.Common;

[Collection("integration-tests")]
public abstract class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory>
{
    protected readonly IFixture AutoFixture;
    protected readonly ApiClient NSwagClient;
    protected readonly IServiceScope Scope;

    public BaseIntegrationTest(CustomWebApplicationFactory factory)
    {
        AutoFixture = new Fixture();
        var client = factory.CreateClient();
        NSwagClient = new ApiClient(client);
        Scope = factory.Services.CreateScope();

        var documentStore = Scope.ServiceProvider.GetRequiredService<IDocumentStore>();
        documentStore.Advanced.ResetAllData().GetAwaiter().GetResult();
    }
}