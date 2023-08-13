using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace TodoWolverine.Api.Tests.Fixtures;

[Collection("integration-tests")]
public abstract class BaseIntegrationTest
{
    protected BaseIntegrationTest(WebAppFixture fixture)
    {
        Host = fixture.AlbaHost;
        var documentStore = fixture.AlbaHost.Services.GetRequiredService<IDocumentStore>();
        documentStore.Advanced.ResetAllData().GetAwaiter().GetResult();
    }

    public IAlbaHost Host { get; }
}